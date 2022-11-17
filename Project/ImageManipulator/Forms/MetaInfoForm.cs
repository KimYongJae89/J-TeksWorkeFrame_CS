using Emgu.CV;
using ExifLib;
using LibraryGlobalization.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageManipulator.Forms
{
    public partial class MetaInfoForm : Form
    {
        private JImage jImg;
        private string path;


        public MetaInfoForm(JImage img) : this(img, img.Path) { }

        public MetaInfoForm(JImage img, string path) : this()
        {
            this.jImg = img;
            this.path = path;

            if (jImg.Format == KiyLib.DIP.KImageFormat.TIFF)
                btnTagView.Visible = true;
            else if (jImg.Format == KiyLib.DIP.KImageFormat.JPEG)
            {
                btnTagView.Text = "EXIF Tag View";
                btnTagView.Visible = true;
            }
            else
                btnTagView.Visible = false;

            UpdateData();
        }

        private MetaInfoForm()
        {
            InitializeComponent();
        }


        private void MetaInfoForm_Load(object sender, EventArgs e)
        {
            this.Text = LangResource.MetaInfo;
            btnTagView.Text = LangResource.TagView;
            btn_Ok.Text = LangResource.Ok;
            btn_Cancle.Text = LangResource.Cancel;
            tabMeta.TabPages.Remove(tabPgFormat);
            tabMeta.TabPages.Remove(tabPgSequenceInfo);
        }

        private void btnTagView_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> tags = new Dictionary<string, string>();

            //이걸로도 되나 exif 태그 다 불러오는게 아니라서 아래 루틴으로 바꿈.
            if (jImg.Format == KiyLib.DIP.KImageFormat.TIFF)
            {
                KiyLib.DIP.KTiff tif = new KiyLib.DIP.KTiff(path);
                tags = tif.GetTagInfos();
            }

            if (jImg.Format == KiyLib.DIP.KImageFormat.JPEG)
            {
                try
                {
                    using (var reader = new ExifReader(path))
                    {
                        var props = Enum.GetValues(typeof(ExifTags)).Cast<ushort>().Select(tagID =>
                        {
                            object val;
                            if (reader.GetTagValue(tagID, out val))
                            {
                                if (val is double)
                                {
                                    int[] rational;
                                    if (reader.GetTagValue(tagID, out rational))
                                        val = string.Format("{0} ({1}/{2})", val, rational[0], rational[1]);
                                }

                                return string.Format("{0}: {1}", Enum.GetName(typeof(ExifTags), tagID), RenderTag(val));
                            }

                            return null;

                        }).Where(x => x != null).ToArray();

                        for (int i = 0; i < props.Length; i++)
                        {
                            string strSrc = props[i];
                            var strPair = strSrc.Split(':');

                            if (strPair.Length == 2)
                            {
                                //tag value앞뒤 공백제거
                                strPair[1] = strPair[1].TrimStart();
                                strPair[1] = strPair[1].TrimEnd();

                                //키 중복 확인
                                if (!tags.ContainsKey(strPair[0]))
                                {
                                    //ResolutionUnit

                                    if (strPair[0] == "ResolutionUnit")
                                    {
                                        if (strPair[1] == "2")
                                            strPair[1] = "inches (Code: 2)";
                                        if (strPair[1] == "3")
                                            strPair[1] = "cm (Code: 3)";
                                    }
                                    tags.Add(strPair[0], strPair[1]);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Something didn't work!
                    MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            if (tags.Count > 0)
            {
                ImageTagViewForm frm = new ImageTagViewForm(tags);
                frm.ShowDialog();
            }
        }

        private void btn_Ok_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Cancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void UpdateData()
        {
            //General 탭
            DateTime time;

            if (string.IsNullOrEmpty(path))
            {
                lbFilePath.Text = "None";
                lbDataModifiedTime.Text = "None";

                if (jImg != null)
                    lbFileSize_Byte.Text = jImg.ImageSize_Byte.ToString("N0");
                else
                    lbFileSize_Byte.Text = "None";
            }
            else
            {
                lbFilePath.Text = path;
                time = File.GetLastWriteTime(path);
                lbDataModifiedTime.Text = time.ToString("yyyy-MM-dd, HH:mm:ss");
                lbFileSize_Byte.Text = new FileInfo(path).Length.ToString("N0");
            }

            //Format 탭
            lbFmt.Text = jImg.Format.ToString();
            //lbXRes.Text = ;
            //lbYRes.Text;

            //Image
            lbImgFmt.Text = jImg.Format.ToString();

            string type = "";
            if (jImg.Depth == KiyLib.DIP.KDepthType.Dt_8)
                type = "8-bit, ";
            if (jImg.Depth == KiyLib.DIP.KDepthType.Dt_16)
                type = "16-bit, ";
            if (jImg.Depth == KiyLib.DIP.KDepthType.Dt_24)
                type = "24-bit, ";

            type += jImg.Color.ToString();
            lbType.Text = type;
            lbImgWidth.Text = jImg.Width.ToString() + " Pixels";
            lbImgHeight.Text = jImg.Height.ToString() + " Pixels";
            lbClrSpace.Text = jImg.Color.ToString();
        }

        private string RenderTag(object tagValue)
        {
            // Arrays don't render well without assistance.
            var array = tagValue as Array;
            if (array != null)
            {
                // Hex rendering for really big byte arrays (ugly otherwise)
                if (array.Length > 20 && array.GetType().GetElementType() == typeof(byte))
                    return "0x" + string.Join("", array.Cast<byte>().Select(x => x.ToString("X2")).ToArray());

                return string.Join(", ", array.Cast<object>().Select(x => x.ToString()).ToArray());
            }

            return tagValue.ToString();
        }
    }
}

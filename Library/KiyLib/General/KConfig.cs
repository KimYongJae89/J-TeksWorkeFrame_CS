using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiyLib.General
{
    /// <summary>
    /// Config(환경설정) 클래스
    /// 싱글턴 패턴으로 구현되어 있다
    /// </summary>
    public class KConfig
    {
        private static volatile KConfig _uniqInstance;
        private static object _syncRoot = new Object();

        /// <summary>
        /// Section의 집합
        /// </summary>
        public List<KConfigSection> SectionList;

        /// <summary>
        /// Config 파일의 경로
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 클래스의 유일한 인스턴스
        /// </summary>
        public static KConfig Inst
        {
            get
            {
                if (_uniqInstance == null)
                {
                    lock (_syncRoot)
                    {
                        if (_uniqInstance == null)
                            _uniqInstance = new KConfig();
                    }
                }

                return _uniqInstance;
            }
        }


        private KConfig()
        {
            SectionList = new List<KConfigSection>();
        }


        /// <summary>
        /// Config파일을 불러온다
        /// Path속성이 공백이면 오류가 발생한다
        /// </summary>
        /// <returns>불러오기 성공여부</returns>
        public bool Load()
        {
            if (string.IsNullOrEmpty(this.Path))
                throw new Exception("Config클래스의 Path속성이 NullOrEmpty 입니다.");

            return Load(this.Path);
        }

        /// <summary>
        /// Config파일을 불러온다
        /// </summary>
        /// <param name="path">파일의 경로</param>
        /// <returns>불러오기 성공여부</returns>
        public bool Load(string path)
        {
            if (!File.Exists(path))
                return false;

            using (StreamReader txtReader = new StreamReader(path))
            {
                string line;
                KConfigSection sct = new KConfigSection();

                while ((line = txtReader.ReadLine()) != null)
                {
                    if (string.IsNullOrEmpty(line))
                        continue;

                    if (IsSection(line))
                    {
                        line = line.Substring(1, line.Length - 2);
                        sct = new KConfigSection(line);
                        SectionList.Add(sct);
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(sct.Name))
                            throw new Exception("Section Name이 NullOrEmpty 입니다.");

                        string key = SplitKeyAndValue(line)[0].Trim();
                        string val = SplitKeyAndValue(line)[1].Trim();

                        sct.AddKeyAndVlaue(key, val);
                    }
                }
            }
            if (SectionList.Count == 0)
                throw new Exception("config 파일의 Section의 개수가 0개입니다.");

            return true;
        }

        /// <summary>
        /// Config파일을 저장한다
        /// Path속성이 공백이면 오류가 발생한다
        /// </summary>
        /// <returns>저장 성공여부</returns>
        public bool Save()
        {
            if (string.IsNullOrEmpty(this.Path))
                throw new Exception("Config클래스의 Path속성이 NullOrEmpty 입니다.");

            return Save(this.Path);
        }

        /// <summary>
        /// Config파일을 저장한다
        /// </summary>
        /// <param name="path">파일의 경로</param>
        /// <returns>저장 성공여부</returns>
        public bool Save(string path)
        {
            List<string> lines = new List<string>();

            using (StreamWriter txtWriter = new StreamWriter(path))
            {
                for (int i = 0; i < SectionList.Count; i++)
                {
                    string sctName = string.Format("[{0}]", SectionList[i].Name);
                    lines.Add(sctName);

                    foreach (var item in SectionList[i].KeyValuePair)
                    {
                        string key = item.Key;
                        string val = item.Value;

                        string keyAndVal = string.Format("{0} = {1}", key, val);
                        lines.Add(keyAndVal);
                    }

                    lines.Add("");  //Section 사이에 한줄씩 띄움
                }

                lines.RemoveAt(lines.Count - 1);  //마지막 공백줄 제거

                foreach (var item in lines)
                    txtWriter.WriteLine(item);
            }

            return true;
        }

        /// <summary>
        /// Section객체를 가져온다
        /// </summary>
        /// <param name="section">Section 이름</param>
        /// <returns>결과 값</returns>
        public KConfigSection GetSection(string section)
        {
            string sectionCL = section.ToUpper();

            KConfigSection sct = SectionList.Find(
               s => s.Name.ToUpper().Contains(sectionCL));

            return sct;
        }

        /// <summary>
        /// Section에 Key와 Value값을 대입한다
        /// </summary>
        /// <param name="section">Section 이름</param>
        /// <param name="key">Key의 이름</param>
        /// <param name="value">Value의 이름</param>
        public void SetValue(string section, string key, object value)
        {
            KConfigSection sct = GetSection(section);
            string keyCL = key.ToUpper();

            if (sct == null)
                throw new Exception("존재하지 않는 Section입니다.");

            int index = sct.KeyValuePair.FindIndex(
                kv => kv.Key.ToUpper() == keyCL);

            bool isExist = (index != -1);
            if (isExist)
                sct.KeyValuePair[index] = new KeyValuePair<string, string>(key, value.ToString());
            else
                sct.AddKeyAndVlaue(key, value.ToString());
        }

        /// <summary>
        /// Config값을 가져온다
        /// </summary>
        /// <param name="section">Section 이름</param>
        /// <param name="key">Key의 이름</param>
        /// <returns>결과 값</returns>
        public string GetValue(string section, string key)
        {
            KConfigSection sct = GetSection(section);
            string keyCL = key.ToUpper();

            if (sct == null)
                throw new Exception("존재하지 않는 Section입니다.");

            int index = sct.KeyValuePair.FindIndex(
                kv => kv.Key.ToUpper() == keyCL);

            bool isExist = (index != -1);
            if (isExist)
                return sct.KeyValuePair[index].Value;
            else
                return null;
        }

        /// <summary>
        /// Config값을 가져온다
        /// Key, Value형식은 원본 Config파일이 텍스트로 저장돼 있어 string형식이다
        /// 이를 특정 변수형으로 좀더 편하게 변환시키기 위해 사용한다
        /// </summary>
        /// <typeparam name="T">변환할 변수타입, T형식으로 반환값이 변환된다</typeparam>
        /// <param name="section">Section 이름</param>
        /// <param name="key">Key의 이름</param>
        /// <returns>결과 값</returns>
        public T GetValue<T>(string section, string key)
        {
            Type t = typeof(T);

            //값 형식이 아니라면
            if (!KCommon.IsValueType(t))
                throw new FormatException("GetValue<T>는 bool, int32, string 타입만 지원합니다.");

            KConfigSection sct = GetSection(section);
            string keyCL = key.ToUpper();

            if (sct == null)
                throw new Exception("존재하지 않는 Section입니다.");

            int index = sct.KeyValuePair.FindIndex(
                kv => kv.Key.ToUpper() == keyCL);

            bool isExist = (index != -1);
            if (isExist)
                return KCommon.ConvertToValueType<T>(sct.KeyValuePair[index].Value);
            else
                return default(T);
        }


        /// <summary>
        /// 문자열이 Section인지 검사한다
        /// Section은 대괄호로 감싸져 있다 (ex) [X-Ray])
        /// </summary>
        /// <param name="line">대상 문자열</param>
        /// <returns>결과 값</returns>
        private bool IsSection(string line)
        {
            if (string.IsNullOrEmpty(line))
                throw new Exception("IsSection 인자가 NullOrEmpty 입니다.");

            if (line.IndexOf('[') == 0 &&
                line.IndexOf(']') == line.Length - 1)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 문자열을 Key, Value로 각각 분리시킨다
        /// (ex)ModelName=XNPI -> key:ModelName, value:XNPI)
        /// </summary>
        /// <param name="param">분리시킬 문자열</param>
        /// <returns>결과 값, 배열 원소는 2개이며 각각 Key, Value순서</returns>
        private string[] SplitKeyAndValue(string param)
        {
            if (IsSection(param))
                throw new Exception("SplitKeyAndValue 인자가 Section값 입니다.");

            string[] kvArr = param.Split('=');

            if (kvArr.Length != 2)
                throw new Exception("SplitKeyAndValue 인자가 잘못된 값 입니다.");

            return kvArr;
        }
    }


    /// <summary>
    /// Config파일의 섹션 클래스
    /// Section은 Key와 Value쌍의 집합이며, Config파일은 1개 이상의 Sectino의 집합으로 이루어져 있다.
    /// </summary>
    public class KConfigSection
    {
        /// <summary>
        /// Section의 이름
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Section내의 Key와 Value쌍(KeyValuePair)들의 집합
        /// </summary>
        public List<KeyValuePair<string, string>> KeyValuePair;


        public KConfigSection() { }

        public KConfigSection(string name)
        {
            this.Name = name;
            KeyValuePair = new List<KeyValuePair<string, string>>();
        }


        /// <summary>
        /// Key와 Value쌍(KeyValuePair)을 추가한다
        /// 중복 처리가 되어있지 않아, KConfig 클래스의 SetValue() 사용을 권장한다
        /// </summary>
        /// <param name="key">Key 값</param>
        /// <param name="value">Value 값</param>
        public void AddKeyAndVlaue(string key, string value)
        {
            var item = new KeyValuePair<string, string>(key, value);
            KeyValuePair.Add(item);
        }
    }
}
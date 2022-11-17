using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// 어셈블리에 대한 일반 정보는 다음 특성 집합을 통해 
// 제어됩니다. 어셈블리와 관련된 정보를 수정하려면
// 이러한 특성 값을 변경하세요.
[assembly: AssemblyTitle("LibraryGlobalization")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Dell Inc")]
[assembly: AssemblyProduct("LibraryGlobalization")]
[assembly: AssemblyCopyright("Copyright © Dell Inc 2019")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]



// *** Friend assembly 선언 ***
// [assembly: InternalsVisibleTo("같은 솔루션에 있는 어셈블리명")]
// 이런 식으로 해주지 않으면 다른 프로젝트(어셈블리에서) 리소스를 가져다 쓸수없다
// 해당 언어리소스를 가져다 쓸 프로젝트 이름들을 전부 추가시킨다
[assembly: InternalsVisibleTo("XManager")]
[assembly: InternalsVisibleTo("ImageManipulator")]
[assembly: InternalsVisibleTo("JTeksEmguCV")]
[assembly: InternalsVisibleTo("JTeksLib")]
[assembly: InternalsVisibleTo("JTeksControls")]



// ComVisible을 false로 설정하면 이 어셈블리의 형식이 COM 구성 요소에 
// 표시되지 않습니다. COM에서 이 어셈블리의 형식에 액세스하려면
// 해당 형식에 대해 ComVisible 특성을 true로 설정하세요.
[assembly: ComVisible(false)]

// 이 프로젝트가 COM에 노출되는 경우 다음 GUID는 typelib의 ID를 나타냅니다.
[assembly: Guid("1150c3dd-378e-41c2-a5d3-cd35065301bd")]

// 어셈블리의 버전 정보는 다음 네 가지 값으로 구성됩니다.
//
//      주 버전
//      부 버전 
//      빌드 번호
//      수정 버전
//
// 모든 값을 지정하거나 아래와 같이 '*'를 사용하여 빌드 번호 및 수정 번호를
// 기본값으로 할 수 있습니다.
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]

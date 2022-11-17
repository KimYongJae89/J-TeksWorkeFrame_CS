* 김인영 *
- 솔루션-
1. ImageManipulator.sln
   - ImageJ같은 이미지처리 프로그램
   - EmguCV사용 (OpenCV를 C#으로 Wrapping한 라이브러리)
2. XNPI - 현재 미구현
   - XNPI MFC C++ 코드를 .Net C#코드로 포팅
   - 중간에 취소되어 미구현으로 남았다

- Library -
1. JMatrox - 현재 일부분만 구현
   - Matrox 라이브러리를 Wrapping한 프로젝트
   - XNPI에 쓰기 위해 개발중, 취소되어 미구현으로 남았다
2. KiyControls
   - 다용도로 쓰일 UserControl들을 위한 프로젝트
3 .KiyEmguCV
   - EmguCV를 사용하여 이미지 처리를 하기위한 프로젝트
   - 현재 ImageManipulator.sln에서 사용중이다
4. KiyLib
   - 영상처리및 기타 자주쓰이는 기능들을 위한 프로젝트
5. LibraryGlobalization
   - 다국어 지원을 위한 언어 리소스 프로젝트
   - ImageManipulator.sln에서 사용중이다.
   - 그외 XNPI.sln, KiyEmguCV.csproj, KiyControls.csproj등에서도 사용한다
6. Language_XNPI
   - 다국어 지원을 위한 언어 리소스 프로젝트
   - XNPI에 쓰기 위해 개발중, 취소되어 미구현으로 남았다
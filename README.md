### .NET WinForm / Html 태그, CSS (WebBrowser 사용)로 커스터마이징 가능한 테이블 컴포넌트
윈폼에서 테이블 형식의 뷰를 사용하고 싶을 때 주로 DataGridView나 유료 테이블 컴포넌트를 사용하게 됩니다.
전자의 경우 디자인이나 기능등의 추가 삭제가 어렵고, 
후자도 전자 보다는 선택의 폭이 넓지만 기능이나 디자인의 추가는 어려운 편이며, 유료라는 단점도 존재합니다.
이에 윈폼의 웹브라우저를 이용해 HTML 태그로 작성한 테이블 (CSS 포함)을 렌더링하고 자바스크립트와 연동하여
C#에서 이벤트를 처리할 수 있게 컴포넌트를 제작해봤습니다.
완성도 보다는 아이디어 구현을 목표로 작성하였습니다.

### 기초 사용법
```
/// <summary>
/// Html 문자열로 테이블 불러오기
/// </summary>
/// <param name="table">HTML 문자열</param>
/// <returns></returns>
public bool LoadTable(string table)
```
Resouce나 html 파일을 읽어와 string 형태로 넘겨 주면 테이블이 렌더링됩니다.

### 정확한 사용법
자유도가 높게 구현하려고 노력하였기 때문에
예제를 통한 사용법 숙지가 편하다고 생각하여
소스 코드에 예제를 포함시켰습니다.

TableHelperTester 프로젝트를 참조

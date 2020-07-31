# BaslerCamTest
Basler 비전 카메라 학습용 리포지토리

BaslerCamTest 프로젝트 : 단순히 카메라 연결 및 콘솔에 몇몇 카메라 파라메터 표시하는 연습용

GrabImageTest 프로젝트 : Basler.Pylon.dll .net 라이브러리 이용하여 카메라에서 이미지 취득하여 bmp로 저장 및 pictureBox에 표시하는 실습
                        주 - 이미지 grab이 안될 경우 pylon viewer에서 몇몇 타이밍/딜레이 파라메터 시간을 늘려줄 필요가 있으며 에러 메세지를 참고바람

GrabOpenCVTest 프로젝트 : GrabImageTest 프로젝트의 결과물에 OpenCvSharp 라이브러리 이용하여 이미지 변환 실습 (작성 중)

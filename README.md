![게임 스크린샷1](https://github.com/JuYongWoo/ARProject/blob/main/Images/ScreenShot1.jpg)
![게임 스크린샷1](https://github.com/JuYongWoo/ARProject/blob/main/Images/ScreenShot2.jpg)
![게임 스크린샷2](https://github.com/JuYongWoo/ARProject/blob/main/Images/ScreenShot3.jpg)


# 프로젝트 소개
- 현재 개발 중인 게임입니다.
- AR환경에서 적들의 공격 부위를 눌러 쓰러트리는 간단한 게임입니다.

# 프로젝트 구조 및 핵심 설계 요약
- UI 내 값을 조정하거나 버튼 이벤트 할당을 간편화하기 위하여 <Enum, GameObject> 형식의 Dictionary를 통해 매핑하여 사용, 관리합니다.
- Prefab이나 AudioClip 등 게임 리소스를 Enum을 통해 매핑할 때도 <Enum, Object> 형식의 Dictionary를 통해 사용, 관리하도록 합니다.

---

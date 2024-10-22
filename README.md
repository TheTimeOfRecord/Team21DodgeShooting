# Team21DodgeShooting

**Bullet Fighters 2024**


## 📖 목차

1. [프로젝트 소개](#프로젝트-소개)
2. [팀소개](#팀소개)
3. [게임구조](#게임구조)
4. [주요기능](#주요기능)
5. [개발기간](#개발기간)
6. [기술스택](#기술스택)
7. [Trouble Shooting](#trouble-shooting)

---
    
## 프로젝트 소개

- 닷지 게임을 베이스로 한 슈팅 게임
전투기를 조종해 총알을 피하고 적 전투기를 파괴하는 게임

---

## 팀소개

- 이영근 (팀장) : 아이템 생성 및 적용, UI - 아이템, 플레이어 체력 및 경험치
- 손형민 : 오브젝트풀 생성, 투사체, 보스몬스터
- 안성찬 : 몬스터 4종류 이동  및 반복 생성
- 최빈 : 시작화면, 캐릭터 선택 기능, 게임 그래픽 제공, 발표자료

---

## 게임구조
![Scene](https://github.com/user-attachments/assets/09684892-59b2-4035-8477-856ddb452d30)


---

## 주요기능

- 기능 1. 플레이어 캐릭터 선택 기능   
![SelectCharacter](https://github.com/user-attachments/assets/fddf9f89-6852-4344-8a54-566bef245695)   
캐릭터를 선택하면 각 PlayerID에 따라 Animation이 적용된다.
  - Manager - GameManager.cs, CharacterSelect - PlayerSelectManager1.cs, Controller - AnimationController.cs   
<br>

- 기능 2. 총알, 적, 아이템 오브젝트를 오브젝트 풀링을 활용하여 생성
  - 오브젝트 풀에서 총알, 적, 아이템을 활성화하고 각각 비활성화 로직을 설정해주었다. 
  - Utility - ObjectPool.cs   
  - Controller - ShootingContoller.cs, EnemyShootingController.cs   
  - Manager - GameManager.cs, EnemyManager.cs
  - Item - ItemSpawner.cs
<br>

- 기능 3. 충돌 감지   
  - Controller - ProjectileController.cs   
	  - 플레이어, 적 - 총알   
      플레이어나 적이 총알과 충돌하면 총알을 쏜 주체가 누구인지 체크하여 데미지를 줄지, 무시할지 결정
	  - 총알 - 총알   
      총알을 쏜 주체가 서로 다르면 파괴된다.
  - Enemy - EnemyController.cs   
    - 플레이어 - 적   
      플레이어와 적이 충돌하면 적은 파괴되며 플레이어에게 데미지를 준다.
<br>

- 기능 4. 아이템
  - Assets - Item폴더
	- 적을 처치하면 아이템이 드랍된다. 아이템을 획득하면 3가지 선택지가 주어진다.
  - 탄환 개수 증가, 체력 회복, 스피드 증가, 최대 체력 증가, 폭탄획득(미구현) 등의 아이템 선택지가 존재한다.   
 ![ItemSeletion](https://github.com/user-attachments/assets/596b0be2-3be7-488a-82c1-5e8fecf2ddb9)   
<br>

- 기능 5. 적 움직임 패턴
  - Straight Enemy : 생성 시점의 플레이어 캐릭터 위치로 일직선으로 날아간다.
  - Tracing Enemy : 플레이어에게 닿을 때까지 쫓는다.
  - Hovering Enemy : 플레이어와 특정 거리까지 가까워졌을 때 주위를 돌며 투사체를 날린다.
  - Blinking Enemy : 카메라 범위 내 랜덤 위치로 순간이동을 한다. 플레이어 위로는 이동하지 않는다.
  - Asset - Enemy - EnemyController.cs, Straight/Tracing/Hovering/BlinkingEnemyController.cs
<br>

- 인게임 장면   
![InGameScene](https://github.com/user-attachments/assets/d00c62bb-865c-4b8e-b171-de16ce2efd75)   

---

## 개발기간

- 2024.10.15(월) ~ 2024.10.22(화)   

---

## 기술스택

- 유니티 2022.3.17f LTS   
- Microsoft Visual Studio 2022   
- GitHub   

---

## Trouble Shooting

<details>
  <summary>오브젝트 풀링  - 자주 반복되는 활성화/비활성화</summary>
    <div markdown="1">
      <ul>
        <li>몬스터의 경우 오브젝트 풀링을 사용하여 활성화/비활성화가 자주 반복되는데, 활성화될 때마다 초기화가 필요한 필드가 생긴다. 이 부분을 고려하지 못해 처음 몬스터가 비활성화되고 다시 활성화 될 때 몬스터가 죽지 않거나 총알을 쏘지 않는 등의 버그가 발생했다.</li>
        <li>이러한 경우에는 유니티 이벤트 메서드 중 하나인 OnEnable을 사용해야 한다.</li>
        <li>Awake – 오브젝트가 활성화된 직후 1회 호출</li>
	    <li>OnEnable – 오브젝트와 스크립트 컴포넌트 모두 활성화된 직후 호출</li>
	    <li>Start – 첫 프레임 업데이트 직전 1회 호출</li>
	    <li>OnDisable – SetActive(false)가 되면 호출</li>
      </ul>
    </div>
</details>

<details>
  <summary>Z축  방향에 대한 부주의</summary>
    <div markdown="1">
      <ul>
        <li>Vector3.Distance로 몬스터와 플레이어 사이의 거리를 구할 때나 플레이어-몬스터의 Direction을 정할 때, z축 값에 부주의하다보니 원하는 움직임이 나오지 않았다.</li>
        <li>예를 들어, Hovering Enemy(적 주위를 공전)의 경우 플레이어와 특정 거리까지 가까워지면 주위를 공전해야하는데 z 값이 특정 거리보다 큰 경우 주위를 공전하지 않고 플레이어 위로 오게 된다.</li>
        <li>z축 값을 플레이어와 동일하게 맞춰주었다.</li>
      </ul>
    </div>
</details>

<details>
  <summary>Transform은 참조형 변수</summary>
    <div markdown="1">
      <ul>
        <li>처음에 게임 오브젝트로 몬스터 SpawnPoint 4곳을 정하고 Transform으로 받아와 그 주변에 랜덤으로 스폰하게 했는데 위치 조정 값을 더하다가 SpawnPoint의 Transform이 계속 변했다</li>
        <li>Position값을 vector2로 받아와서 해결(z값도 무시)</li>
      </ul>
    </div>
</details>

<details>
  <summary>자체 피드백</summary>
    <div markdown="1">
	<p>1. 가독성, 유지보수성, 확장성이 부족하다.</p>
	<p> - HealthSystem, StatHandler와 같이 플레이어와 적이 공통적으로 사용하는 클래스에서 플레이어와 적이 각각 필요한 기능을 모두 담아 가독성이 떨어졌다.</p>
	<p> -> 공통된 부분만 담고, 플레이어와 적을 구분한다. </p>
	<p> - 아이템을 적용하기 위해 플레이어와 관련된 필드 및 메서드가 곳곳에 퍼져있다보니 찾기가 힘들었다.</p>
	<p> -> 플레이어를 담당하는 클래스를 만들고 인터페이스를 활용하여 플레이어와 관련된 필드 및 메서드를 담당하여 유지보수성, 확장성을 확보한다.</p>
	<p>2. 플레이어 선택 시 플레이어를 프리팹에서 생성</p>
	<p> - 현재는 플레이어 오브젝트의 애니메이션을 선택에 따라 바꿔주었다. 이때, 플레이어 캐릭터를 프리팹으로 만들어 사용하는 것에 대해 고려했었다.</p>
	<p> -> 플레이어 캐릭터마다 다른 능력을 부여하고 싶은 경우나 2p 게임으로 확장될 경우를 고려하면 플레리어를 프리팹에서 생성하는 것도 좋을 것 같다.</p>
    </div>
</details>

---

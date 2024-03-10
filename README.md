# VR_Fes_Project_Tips
  
【概要】  
ハッカソン（2023年12月16日）とVRフェス（2024年2月24日）で公開したARたけとんぼ、AR脳内ハッカーシミュレータの部品集  
主に制作物のコンポーネント紹介を目的としたプロジェクトとなります  
  
【動作環境】  
Unity2022.3.14f1  
Meta Quest3（MetaQuestビルド62.0）  
  
【利用方法（QuestLink使用）】  
1. MetaQuest単体で起動し、ルームの設定を行ってください。  
2. QuestLink接続の前にQuestPCアプリ上で[Setting]->[ベータ]において、以下の項目にチェックを入れてください  
　・開発者ランタイム機能 [ON]  
　・Passthrough over Meta Quest Link [ON]  
　・Spatial Data over Meta Quest Link [ON]  
3. MeataQuestとPCをケーブル接続しQuestLink接続を行った後、Unity画面の再生ボタンを押下してください。  
  
【操作方法】  
 起動後に目の前に表示された操作パネルから各ボタンを押下して操作を行ってください  
  
【機能の説明】  
・[MoveLeft] 操作パネルを左に移動する  
・[MoveRight] 操作パネルを右に移動する  
・[GizmosSphere] 自身を中心とした球面にオブジェクト配置  
・[Far] GizmosSphereを少し遠くする  
・[Near] GizmosSphereを少し近くする  
・[GizmosSphereDistance] スライドの値に応じてGizmosSphereの距離が変化する  
  
・[Taketonbo] ARたけとんぼ発射処理  
  
・[BigEnter] 大きなEnterキーを配置（押せる）  
・[KeyBoard] キーボード&モニターを配置（キー入力&表示可能）  
・[RealPanel] 自身を中心とした球面に現実空間が透けて見えるウィンドウをランダム配置  
・[AngleSphere] 掌で押し込んで回転操作を行うオブジェクト配置（掌で押し込んで回す）  
・[SliderBar] 指を上下・左右にスライドさせて操作するコントロール配置  
・[FloorOn] 床面にオブジェクトを配置  
  
・[Exit] アプリ停止  
・[DestroyObject] 上記ボタンから表示したオブジェクトをすべて破棄  
  
・その他、ワールド原点と起動時の目線の位置にギズモっぽいスフィアを表示させています（位置確認用です）  
  
【スクリプトを確認したい場合】  
基本的に、GameManager→○○Manager→△△Creater→□□オブジェクト（プレハブから生成）の順番になっていますのでその順番で参照を追っていただければ。  
  
【使用アセット】  
・MetaXEAll-in-One SDK　60.0.0  
・ProBuilder　5.2.2  
  
【素材】  
・効果音ラボ様　https://soundeffect-lab.info/  
  
 【その他】  
 ・パススルー機能を実現するため、Unity-TheWorldBeyondサンプルプロジェクトの一部スクリプトやマテリアルを使用しています（Assets/Controllers/References）  
　https://github.com/oculus-samples/Unity-TheWorldBeyond

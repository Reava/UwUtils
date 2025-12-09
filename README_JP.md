
<div align=left>
<a href="https://reava.github.io/UwUtils/"><img alt="VCCに追加" src="https://github.com/user-attachments/assets/33d583a7-4f7f-426a-901a-1581bd98001e"></a>
<img alt="GitHub ワークフローのステータス" src="https://img.shields.io/github/actions/workflow/status/reava/UwUtils/release.yml?style=for-the-badge">
<img alt="GitHub" src="https://img.shields.io/github/license/Reava/UwUtils?color=blue&style=for-the-badge">
<a href="https://github.com/Reava/UwUtils/releases/latest/"><img alt="GitHub リリース (日付順の最新)" src="https://img.shields.io/github/v/release/reava/UwUtils?logo=unity&style=for-the-badge"></a>
<a href="https://github.com/Reava/UwUtils/releases/latest/"><img alt="GitHub 全リリース" src="https://img.shields.io/github/downloads/reava/UwUtils/total?color=blue&style=for-the-badge"></a>
</div>
<br>
<a href="https://github.com/Reava/UwUtils/blob/main/README.md">English</a> | <a href="https://github.com/Reava/UwUtils/blob/main/README_JP.md">日本語</a>
<br>

# 🧰 Reava_のUdon UwUtilsツールキット

* 様々なプロジェクト向けに私がU#で作成したニッチなスクリプトを、再検討・整理して皆さんが使えるようにしたものです。
* このツールキットは、高度に特化したり過剰に設計されたスクリプトではなく、シンプルなスクリプトを組み合わせて特定の動作を創出するために作られています！
* どうぞご自由にコードの一部を引用して独自スクリプトを作成してください！プロジェクトに直接活用したり、新たなスクリプトへと発展させるための便利なツールボックスとしてお役立てください。

注記：スクリプトは全て英語で記述されています。各スクリプトの簡単な説明については、下記のドキュメントを参照してください（日本語）

V1.0のリリースは2026年を予定しています（計画中の機能：同期、永続化、新規スクリプト、UIサンプル、コードのクリーンアップ）

**情報**: UwUtilsには現在33個のUdonスクリプトが含まれています！アイデアが見つかり次第、今後も拡充していきます。

## ℹ️ **ヒント**: 
- Udon動作を追加する前に、コンポーネントウィンドウに直接入力するだけで、私のスクリプトを簡単に追加できます！
- 多くのスクリプトは他のUwUtilsスクリプトと併用すると効果的です。組み合わせることで非常に多くのことが実現できます！
- 多くの変数にはツールチップが設定されており、ホバーすると詳細を確認できます
- 私のスクリプトは適切なエラーとログを出力します。ログ/コンソールで「*Reava_/UwUtils/*」を検索すると、問題の特定や動作の詳細把握が可能です。

問題が見つかった場合、サポートが必要な場合、または追加してほしいスクリプトがある場合は、私の**[Discord](https://discord.gg/TxYwUFKbUS)**に参加するか、Githubでイシューを開いてください！

## 📥 <a href="https://reava.github.io/UwUtils/">VCCにパッケージとして追加！</a>

## 📋 **スクリプト一覧と説明**
<details>
<Summary>詳細を表示！</summary>

- **[Player Teleporter](https://github.com/Reava/Reava/UwUtils/blob/main/Runtime/Scripts/PlayerTeleporter.cs)**:
文字通り、インタラクト時にテレポートするだけです。
- **[Objects Toggle](https://github.com/Reava/Reava/UwUtils/blob/main/Runtime/Scripts/ObjectsToggle.cs)**:
GameObjectの配列の状態を切り替え、永続化対応済みです！
- **[Object State Setter](https://github.com/Reava/Reava/UwUtils/blob/main/Runtime/Scripts/ObjectStateSetter.cs)**:
インタラクト時に、GameObjectの配列の状態を設定します。一度トリガーされると元に戻らず、状態を設定するのみで同期されません。（イベント: 逆操作には\_Invert、全状態の切り替えには\_Switchを使用）
- **[Tag Assigner](https://github.com/Reava/Reava/UwUtils/blob/main/Runtime/Scripts/TagAssigner.cs)**:
 関数付きホワイトリストとして機能し、ワールド参加時にユーザー名と動作のユーザー配列が一致した者にタグを割り当てます。ローカルで、ユーザーが一致した場合に特定のオブジェクトを切り替え可能。インスタンス作成直後のユーザーにはホワイトリスト一致に関わらず権限付与する切り替え機能あり。ワールド更新なしでホワイトリスト更新可能なリモート文字列の動的追加をサポート！
- **[Tag TP](https://github.com/Reava/Reava/UwUtils/blob/main/Runtime/Scripts/TagTP.cs)**:
名前が正しいタグを所持している状態でこの動作とインタラクトすると、ターゲット地点へテレポートします。所持していない場合はセカンドターゲット地点へテレポート（空欄/無効時はテレポートせず）
- **[ReflectionProbeController](https://github.com/Reava/Reava/UwUtils/blob/main/Runtime/Scripts/ReflectionProbeController.cs)**:
リフレクションプローブは便利！リアルタイム化・スクリプト化して、このスクリプトで更新頻度を変更しよう！ToggleLoop()でループ更新か一時停止（再有効化まで）を切り替え可能
- **[Spinny](https://github.com/Reava/Reava/UwUtils/blob/main/Runtime/Scripts/Spinny.cs)**:
任意の軸で、任意の速度で、さらには奇妙な更新速度（例：30度回転だが1秒に1回だけ）でも回転させるスクリプト。アニメーターでやるべきですが、これは便利かもしれません。
- **[Unity Fog Toggle](https://github.com/Reava/Reava/UwUtils/blob/main/Runtime/Scripts/UnityFogToggle.cs)**:
UnityのフォグをON/OFFするだけのインタラクティブトグルです…それだけ。トリガーやUIボタンで呼び出せます。
- **[Scene Initializer](https://github.com/Reava/Reava/UwUtils/blob/main/Runtime/Scripts/SceneInitializer.cs)**:
ユーザーがワールドに入った最初の数秒間だけオブジェクトを有効化したい？その後無効化？逆？両方？これを使えば簡単です
- **[Tag Setter](https://github.com/Reava/Reava/UwUtils/blob/main/Runtime/Scripts/tagSetter.cs)**:
インタラクト時にローカルユーザーに事前定義のタグを設定します。以上です。
- **[Tag Debugger](https://github.com/Reava/Reava/UwUtils/blob/main/Runtime/Scripts/TagDebugger.cs)**:
ローカルユーザーのタグを表示し、デバッグログやテキストに出力する便利なツール（UnityUI、TMP、TMP GUIと互換性あり）。インタラクト時と起動時に更新されます。
- **[Tag Array TP](https://github.com/Reava/Reava/UwUtils/blob/main/Runtime/Scripts/TagArrayTP.cs)**:
多数のタグがあり、それぞれでユーザーを異なる位置にTPさせたい？ これですべて解決！ユーザーにタグがない場合のフォールバックターゲットも設定可能 （一致するランクが見つからない場合のTPを禁止する設定も可能）
- **[Event Relay](https://github.com/Reava/Reava/UwUtils/blob/main/Runtime/Scripts/EventRelay.cs)**:
ボタンで別のボタンを押したい？UIと同じ操作をしたい？はい、イベント名（例：「\_interact」）を入力し、遅延の有無や秒数を指定するだけで完了！他のオブジェクトの状態を確認し、オン/オフに応じて遅延を無視することも可能です。UdonSharp1.0アップデートではUdonBehavior配列のサポートを予定
- **[Udon Keybinds](https://github.com/Reava/Reava/UwUtils/blob/main/Runtime/Scripts/UdonKeybinds.cs)**:
キーバインドに基づき6種類のUdonビヘイビアへイベント呼び出しを送信。RollTheRedのカメラシステム用、またはコードテンプレートとして機能します。CTRL + 1～6で変更をトリガー。CTRL + 0でキーバインドON/OFFを切り替え（変更時以外はデフォルトON）。UdonSharp1.0アップデートではUdonBehavior配列のサポートを予定
- **[Animator Driver](https://github.com/Reava/Reava/UwUtils/blob/main/Runtime/Scripts/AnimatorDriver.cs)**:
インタラクト時にアニメーターのブール値を反転させます...以上です（永続化対応済み！）
- **[Trigger Zone Relay](https://github.com/Reava/Reava/UwUtils/blob/main/Runtime/Scripts/TriggerZoneRelay.cs)**:
トリガーコライダーを割り当て、Udon Behaviorを割り当てて、進入時または退出時にイベントを送信します。非常にシンプルな機能です！スクリプトのU# 1.xバージョン向けにUdonBehaviorArraysをサポート。
- **[Playercount To Animator](https://github.com/Reava/Reava/UwUtils/blob/main/Runtime/Scripts/PlayercountToAnimator.cs)**:
インスタンス内のプレイヤー数に応じて、アニメーターのパラメーター（1つのビヘイビアにつき1パラメーター、複数のアニメーターを同時にサポート）を2つの値（最小/最大）間で駆動します。プレイヤー数の上限を設定して最大値に到達させることが可能です。
- **[Join Bell](https://github.com/Reava/Reava/UwUtils/blob/main/Runtime/Scripts/JoinBell.cs)**:
非常にシンプルです。オーディオソースと参加/離脱用のクリップをタップするだけで利用可能（ベル音はUIまたはイベントリレーの「\_JoinToggle」イベントで切り替え可能）
- **[Toggle Canvas](https://github.com/Reava/Reava/UwUtils/blob/main/Runtime/Scripts/ToggleCanvas.cs)**:
iStateと同様ですが、Canvasコンポーネント向けです
- **[MeshRenderer Swapper](https://github.com/Reava/Reava/UwUtils/blob/main/Runtime/Scripts/MeshRendererSwapper.cs)**:
実行時に2つのメッシュレンダラーグループ間で切り替えを可能にし、デフォルトで1と2を設定。Questではどちらのグループをデフォルトとするか設定可能。最適化トグルに実用的。イベントをサポート（\_switchGroup, \_enableOne, \_enableTwo）
- **[Instance Creator Relay](https://github.com/Reava/Reava/UwUtils/blob/main/Runtime/Scripts/InstanceCreatorRelay.cs)**:
ローカルユーザーがインスタンスを作成した際に、任意のカスタムイベントをUdon Behaviorsへ送信します
- **[Fading TP](https://github.com/Reava/Reava/UwUtils/tree/main/FadingTP)**:
フェードイン/アウトのブラックアウト効果付きで無制限のテレポートを設定できる小型プレハブ！（ドアごとにフェード速度変更可能、超軽量）
- **[Spawn Fade](https://github.com/Reava/Reava/UwUtils/tree/main/SpawnFade)**:
参加時に世界にフェードインするための小型プレハブ。リスポーン時もフェードインするよう切り替え可能！（実行時に切り替え可能、フェード速度変更可）
- **[Remote String To Text](https://github.com/Reava/Reava/UwUtils/blob/main/Runtime/Scripts/RemoteStringToText.cs)**:
URLからリモート文字列を読み込み、配列や任意のテキストフィールドに出力可能（ご自身の用途に合わせてコードベースとしてご自由にお使いください！）
- **[Sequencial Toggle](https://github.com/Reava/Reava/UwUtils/blob/main/Runtime/Scripts/.cs)**:
一連のオブジェクトを順番にトグルします。異なるイベントを送信して完全にトグルし、進行状況を保持できます。インタラクションで順に切り替わります。
- **[Advanced UI Toggle](https://github.com/Reava/Reava/UwUtils/blob/main/Runtime/Scripts/AdvancedUIToggle.cs)**:
トグル操作に関連する全ての機能を単一ビヘイビアに統合。UIボタン、物理イベントボタン、実際のUIボタンで操作可能。UI関連要素の変更＋サウンドフィードバックを全てトグル可能。
- **[Collectible](https://github.com/Reava/Reava/UwUtils/blob/main/Runtime/Scripts/Collectible.cs)**:
インタラクション時に[収集システム](https://github.com/Reava/Reava/UwUtils/blob/main/Runtime/Scripts/CollectionSystem.cs)へ値を送信し、その収集アイテムの所持数を増加させます
- ** [Collection System](https://github.com/Reava/Reava/UwUtils/blob/main/Runtime/Scripts/CollectionSystem.cs)**:
[収集品](https://github.com/Reava/Reava/UwUtils/blob/main/Runtime/Scripts/Collectible.cs)からイベントを受信し、受け取った全値を合計する中枢。複数テキスト表示への出力が可能
- **[Multi UI Toggle Manager](https://github.com/Reava/Reava/UwUtils/blob/main/Runtime/Scripts/MultiUIToggleManager.cs)**:
任意の数のトグルを相互にリンクし、いずれか一つで他の全てを制御可能にします。新たな値を受信するとスクリプトを更新するため、例えば複数のトグルで単一スクリプトを制御できます。
- **[Multi UI Slider Manager](https://github.com/Reava/Reava/UwUtils/blob/main/Runtime/Scripts/MultiUISliderManager.cs)**:
任意の数のスライダーを相互にリンクし、いずれかのスライダーで他のスライダーを制御可能。新しい値を受信するとスクリプトを更新するため、複数のスライダーで単一の要素を制御できます。
- **[PostProcessing Controller](https://github.com/Reava/Reava/UwUtils/blob/main/Runtime/Scripts/PostProcessingController.cs)**:
単一のスライダー、または同一値を制御する複数スライダー用の[スライダーマネージャー](https://github.com/Reava/Reava/UwUtils/blob/main/Runtime/Scripts/MultiUISliderManager.cs)に基づき、ポストプロセッシングの重みを制御します。
- **[Slider Saver](https://github.com/Reava/Reava/UwUtils/blob/main/Runtime/Scripts/SliderSaver.cs)**
スライダーを保存し、永続化設定で再接続時に最後の既知値を復元します。プレイヤーデータを消去せずにスライダー値をリセットして保存する「\_resetValue」関数をサポート。
- **[Audiolinked Animator](https://github.com/Reava/Reava/UwUtils/blob/main/Runtime/Scripts/AudiolinkedAnimator.cs)**
履歴とポーリングレートを備えたオーディオリンクバンドでアニメーターの浮動値を駆動可能。これにはオーディオリンクでリードバックが有効化されている必要があります！
- **[Instance Time Actions] > 未対応 <**
インスタンス時間（セグメント化）に基づくアクションを実行可能にします。遅れて参加したユーザーとも同期されます。

</details>

## ⚠️ **必要条件**
問題を報告する前に更新を確認してください。

- **[Unity](https://docs.vrchat.com/docs/current-unity-version)** (テスト環境: v2022.3.22f1)
- **[VRChat Worlds SDK3](https://vrchat.com/home/download)** (テスト環境: v3.8.0)
- **Text Mesh Pro** は一部スクリプトで使用され、広く普及しているため、インポートしてください。

## 🔗 **リンク**
サポートと支援はこちら：
- https://discord.gg/TxYwUFKbUS
- https://patreon.com/Reava

チュートリアルはYouTubeに投稿される場合があります：https://www.youtube.com/channel/UCm3RYWUql-2yGt8K2u9eFEg/
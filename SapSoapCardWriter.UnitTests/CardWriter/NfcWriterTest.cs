using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SapSoapCardWriter.BusinessLogic.NFC;
using Moq;
using SapSoapCardWriter.Logger.Logging;
using SapSoapCardWriter.BusinessLogic;
using SapSoapCardWriter.UnitTests.Helper;
using System.Threading.Tasks;

namespace SapSoapCardWriter.UnitTests.CardWriter
{
    [TestClass]
    public class NfcWriterTest
    {
        [TestMethod]
        public void Foo()
        {
            ILogger logger = new ConsoleLogger();
                
            SmartCardChannel scard = new SmartCardChannel(logger, "OMNIKEY CardMan 5x21-CL 0");
            bool present = scard.CardPresent;
            Console.WriteLine("present: {0}", present);
            var beforeConnect = scard.Connected;
            Console.WriteLine("beforeConnect: {0}", beforeConnect);
            bool connectMEthod = scard.Connect();
            Console.WriteLine("connectMEthod: {0}", connectMEthod);
            var afterConnect = scard.Connected;
            Console.WriteLine("afterConnect: {0}", afterConnect);

            var d  = scard.Disconnect();

        }

        [TestMethod]
        public void TestGetUid()
        {
            ILogger logger = new ConsoleLogger();
            NfcWriter writer = new NfcWriter(logger);

            byte[] rfid = writer.GetCardUID("FADDDEADFADDDEAD");

            Console.WriteLine("RFID length:");
            Console.WriteLine(rfid.Length);
            Console.WriteLine("RFID:");
            Console.WriteLine(BitConverter.ToString(rfid));
        }


        [TestMethod]
        public void TestErase()
        {
            ILogger logger = new ConsoleLogger();
            NfcWriter writer = new NfcWriter(logger);

            writer.Erase("FADDDEADFADDDEAD");
        }

        [TestMethod]
        public void TestPrepare()
        {
            ILogger logger = new ConsoleLogger(); 
            NfcWriter writer = new NfcWriter(logger);

            writer.Prepare("FADDDEADFADDDEAD");
        }

        [TestMethod]
        public void TestWrite()
        {
            ILogger logger = new ConsoleLogger(); 
            NfcWriter writer = new NfcWriter(logger);

            List<string> testDataList = new List<string>();
            testDataList.Add("testdata1");
            testDataList.Add("testdata2");
            writer.WriteNfcTag(testDataList);
        }

        [TestMethod]
        public void TestLock()
        {
            ILogger logger = new ConsoleLogger(); 
            NfcWriter writer = new NfcWriter(logger);

            writer.Lock("FADDDEADFADDDEAD");
        }


        [TestMethod]
        public void TestFullWrite()
        {
            ILogger logger = new ConsoleLogger(); 
            NfcCardWriter writer = new NfcCardWriter(logger);
            List<string> testDataList = new List<string>();            
            testDataList.Add("encpublicdata");
            testDataList.Add("encfulldata");

            string sn = writer.GetSerialNumber();
            writer.WriteCard("FADDDEADFADDDEAD", testDataList);

            Console.Write(sn);
        }

        [TestMethod]
        public async Task TestRead()
        {
            ILogger logger = new ConsoleLogger();
            NfcCardWriter writer = new NfcCardWriter(logger);
            List<string> ret = await writer.ReadNfcTags();
            Console.WriteLine(string.Join(",", ret));
        }

        [TestMethod]
        public async Task TestReadWrite()
        {
            try
            {
                ILogger logger = new ConsoleLogger();
                NfcCardWriter writer = new NfcCardWriter(logger);
                List<string> ret = await writer.ReadNfcTags();
                Console.WriteLine(string.Join(",", ret));
                List<string> testDataList = new List<string>();
                testDataList.Add("encpublicdata");
                testDataList.Add("encfulldata");
                writer.WriteCard("FADDDEADFADDDEAD", testDataList);
                Console.WriteLine("Write ok");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }


        [TestMethod]
        public void TestFullDataLoadTest()
        {
            ILogger logger = new ConsoleLogger();
            NfcCardWriter writer = new NfcCardWriter(logger);

            List<string> testDataList = new List<string>();
            testDataList.Add("波쫤緒龒뺋㪉얽䍺䕐䌫䘞〖뜿䰚娬盁僣ㄇ䝢揾旆씘❍֊쿸懾핕☐摎物쮖⧉罛ᯯ縗뿲㖶䘏褒쪺끯欷쳋ᷩ粤盜⎻㿀⎞쯰퓬櫆繀敻顆즗䪊❶몾梨ࡡЌ뱹᠘徐荆ࢂ㫤徰ᢶ뉨矽㣐䷁ﮁ㔷絪ユ쯒꾾잂벧祺❨쨩뵝২꒢ᄋ☐鎁柘鼓ᾪ㔨⯲搈錦뮤ፑ툳䅜즭⸆狫ᾰ聶肋궜ƒ㒺亶੖迚ᮀ㶬ᩕ丿⾤䦺⃶䷌㒦䐵聺哨諕Ľ츟陃羇魭삍䲸䵰蜠₆癐饇㾇죳섚硐㎙棠瘄쫛跈끱㉦☐㌖걂☢데쑡俫᯾侮얶☐裻塇郱爰蒖☸ꍗ靻甩䠋탃罎渃✭瘷┾䝡䱧꧖⢾담䫷㲜᳁༱訯翰뢮㽮挢夿䠗숇莽侄꠯☐샆਍쿪巻ꮦ좥庡倐ਔ咳淜ତ๛我⬦ᕕ貐쁏ᐜ뼧┦纡☐랂ᔉ쵎ꛒ᎑抺Თ㞊湘ﻴꁳ☐퉶䁨㬟馥⣁랲춾覕饊땿庨著뫓涨䲚狂嗄웥䟳愞渲钆ﳙ☐ゎ垌ᄯ꺕郆늁뉧䚯崣揽귃緶﯁욲ﻣ껐☐肞ູ삨⮓⋴Ⱳ鎸硱隐ᘱ嗽发☐Ō哀퟽ඐ⅔㞛൯䫦媓鷨螟頪䋃狤휊䎜銾鐏㺹㒘䬼霍뙣唲砛繀䖮署岮풧垠⌿㝆ၵ搡箜옳笉판덺翻☐☗☐婟奄뱮㫋༰뗝⼰폶嬊钧琠쀈ᵧ특淎됶뀌⪈ᩇ☐幂竡妲忚벳⏚슀馶筒争떯揕备㮳꽷勤㧜컨둍ݱ艦霂嬵픭ﶄ훈喎ꙫ圍㦻銿泌ᲊ筪봒仹囔梼▆齇ᏼ껺ꢵ龈㼎ㆁꝱ≏㭪ɼ筰௶智䓝锿닍刄✬ੜ焅㵳❜뽵魘ⴙ佥윥紥Ⱓ醔䨞歺깠⌿甓靋雷饷룲풻顷☐䞢☐ᷝ舏㹠䳪훞흅棣衱ꯕ軪ʻ挤滑竝네䲲粂鳨持쇳Ὁꃑᵛ掻찛啄韊毫ﷇ庹㯳冢☐㰏獱俶缙ၚ匳絕☐耱식㽞칑홂뤨ģ鑎㼸꘷錄죁爨햋戱댰鶔⢉졎ﾀ簥ᄓ甁嵹ꛗ㷌솱橊൬陿☐䌯ḱꝘ佹䉼忍称컚䗞থ䎶⫤瑗菊๲ﾊ㒿辦Ჯ⬗鍘튡灬維⚧㈚㷨뎉೘폧믆쉽ᐣ讘㭙搌몱જ᝖技ꥠ☐䣹뽥㞋ૌȟ䉩荲㘦ѱ駲顇ܻ頌ퟄ擦ﶰれ猖☐鏤˘▢᪄事⸝擦쨵熅ᗵ讎뇃鍌莍ገớ祮䯎띖☐籔姘Ꙩꢄ䋲밨⼤쉥䵥㵨☐ꛟ沑洋젞쀢䎌☐＞ᗌࠥ☐ꁥ阬☐늖ᦀท됕ভ䁯䷞稿欎繝鉹쪪殮뛦奂б滠☐葖븯䵃퍰蕾逎꿅៝懺㺤喉酞䁰⭺勞ﾝ㵤녪ؑ쟢ᙎ䇊洬퀌骡텭洏ᤇ挍豵뤺ᯣ곦袔ދꕨ맸䮍볈譄ꉡ⺝盪촟⽖ᇙ耺䮀㞭⭔就䇪毊즺꣦婭隘뚀鲬⼅遼퉓託坸䇧ꍰ饞老챜雓埻㏰☐摜鑷㹮憎㾘☐겆键㦌订껰ᆞ歛鳣⨳︀㡾㉿²ښﷰ侸傱丐詞ፌ⼠䩇捰⍂ﵹꎁ裥쩊ᦐ☐ࡂє勜㱹Ⅰ繷썾ᑖ좁굗搔ዘ㫲ㄗ⏛慗뇦☐Ȏ혯ꛃ⍟ἶ飞፦큇禋鞮뵸൙坏䵄Ǎ５䂯☁⒫荷▋☐믒ی늓晖♿믅⬼ⵛ쥧쁗͒ﴶḛ묕臵ꠣ鷯카⌙☐聵ቘ☐騼靋耣馄⤏꒑豈伄ᙧ꼢૕약魲馭툃灪牾叽系囼樫טｖﾥਞ⛷徹ï絛럚爩愑ᩴ畟㣝〗⽁℅ꃯᬭ陱䢉൩㙶蕔闾衸껄垸鞖膚縵餫喊傢딣왟Ŗ痺ķ녅☐䒸Ȭ썤촾Ⲉꅁ緐鎏왇砚἞᳧뒫฾☐賊䋈檳륫䲘⌕鎹ᶝ럵餥쩬蒀푾론靕밮∨嬄䣔貴㚰ൔ첂见뺁糢ꪝ缁雳㊤댂Ჯ曖쾑⭠櫿繌ࡳ板腘濄滛糼ꩆ츘㗋愖ꗎ헰࿰ꘘﭗ톯弯嘬쯴轧蚹퇖뻽唴饾檃풦⧳퓋ꨎ촊頔⊍橗㵚锜郸㭯藃뼔砍퍭შ嵊ﵲ閌㴒㇚꟥☐ۂ㬮퐘喝৴솩㉈䰠졦夅錬ⲑ饒鉲☐踬拊燑톿ᚷムི舾켳⿷巻햕嚔ㄲ팭ꏧ꼫吅曾殏潆䲬걙︭۱뼱☐먲ᯑ몤忕쒧僎⎯ꮣ˺ᷱ궣뿲诤㇁힃Ḩꎻꢹ⍀䁢뀠䡝莥䭁冾꼦ヨ낦䪑쪋☐ⲿ똨ꕭᰙ♹买탱黰湘׎嗤阙넪؆衄芠첿ㅡꯋ꽵嫹ꅑ੍햡䁮㏉㧙薜뇖⹊ￕ蠯뀝羄㳉ᆾ舣鵣┘Ƞ瓤ꝲ楮㒙阠졭陷滞☐䭓酻縘鉀霭꣈癵왓༯뛀ﶶ蓔៥唽譖琰脍ឲቸ旝躢；滾ᄔꆣ跍㜶㈘▱圴䙵㽪ᬀ붇☐齮淫㇞㴮Ⲁ㞜俱㭌⥍଻혂䝠잟瓮麂臬컝㓙씑噎⬨镡缂읪낇泎ᗇ戅俰능蒕ɬ羌溵궂ਂ뎌ᤗ놃ऋ㰷둅⪈空ᩔ슿ល⌃ຐꑨ뱧휚쌵᫬Ƥ倠勲፟⦷☐᝻伧휗⑿实擏");

            writer.WriteCard("FADDDEADFADDDEAD", testDataList);
        }

        [TestMethod]
        public void TestGetSerialNumber()
        {
            ILogger logger = new ConsoleLogger();
            NfcCardWriter writer = new NfcCardWriter(logger);

            var sn = writer.GetSerialNumber();
            Console.WriteLine(sn);
        }
    }
}

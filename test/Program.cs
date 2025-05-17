using System;
using System.Collections;
using System.Reflection;
using System.Threading.Tasks.Dataflow;

namespace MyApp
{
    internal class Program
    {
        static int maxPlayerHP = 200;
        static int PlayerHP = 200;
        static int maxPlayerMP = 100;
        static int PlayerMP = 100;
        static int PlayerPower = 100;
        static int PlayerDefense = 25;
        static int PlayerMagicDamageRandom = 150;
        static int EnemyHP = 500;
        static int Eenemydefense = 55;
        static int EnemyPower = 50;
        static int DebuffA = 0;
        static int RemainingDebuffA;
        static string DownA = "";
        static string DownD = "";
        static int DebuffD = 0;
        static int RemainingDebuffD;
        static bool EnemyCold = false;
        static bool Defensetrue = false;
        static string Action = "";
        static string magic = "";
        static int RemainingPower;
        static int Protection = 1;
        static int RemainingProtect;
        static int IntermediateDamage;
        static bool Enemy2actions = false;
        static string protect = "";
        static string Power = "";

        static void Main()
        {
            while (PlayerHP > 1 || EnemyHP > 1)
            {
                //プレイヤーのターン
                Console.WriteLine("");
                PlayerTurn();
                Console.WriteLine("");
                Thread.Sleep(500);
                if (EnemyHP < 1)
                {
                    Console.WriteLine("あなたはEnemyに勝利した!!");
                    End();
                    break;
                }
                //Enemyのターン
                Random rnd = new Random();
                int EnemyAction = rnd.Next(1, 4);
                if (EnemyCold != true)
                {
                    Console.WriteLine("Enemyの行動");
                    Thread.Sleep(500);
                    switch (EnemyAction)
                    {

                        case 1:
                            Attack2();
                            break;
                        default:
                            Attack1();
                            break;
                    }
                    Thread.Sleep(500);
                    Enemy2actions = true;
                    EnemyAction = rnd.Next(1, 4);
                    switch (EnemyAction)
                    {
                        case 1 :
                            Attack1();
                            break;
                        case 2:
                            Attack2();
                            break;
                        case 3:
                            Attack3();
                            break;
                    }
                    Thread.Sleep(1000);
                    Enemy2actions = false;
                }
                if (Defensetrue == true)
                {
                    Defensetrue = false;
                    PlayerDefense -= 50;
                }
                if (Defensetrue == true)
                {
                    Defensetrue = false;
                }
                if (PlayerHP < 1)
                {
                    Console.WriteLine("あなたは死んだ!!");
                    End();
                    break;
                }
                if (RemainingPower > 0)
                {
                    RemainingPower--;
                    Power = "パワー";
                    if (RemainingPower == 0)
                    {
                        PlayerPower = 100;
                        Power = "";
                        RemainingPower--;
                        Console.WriteLine($"パワーが消えた、強くなっていた力が元に戻った");
                        Thread.Sleep(500);
                    }
                }
                if (RemainingProtect > 0)
                {
                    protect = "プロテクト";
                    RemainingProtect--;
                    if (RemainingProtect == 0)
                    {
                        Protection = 1;
                        protect = "";
                        Console.WriteLine($"プロテクトが消えた、守られている感覚が消えた");
                        Thread.Sleep(500);
                    }
                }
                if (RemainingDebuffA > 0)
                {
                    DownA = "ダウンA";
                    RemainingDebuffA--;
                    if (RemainingDebuffA == 0)
                    {
                        DebuffA = 0;
                        DownA = "";
                        Console.WriteLine($"ダウンAが消えた、弱くなっていた力が戻ってきた");
                        Thread.Sleep(500);
                    }
                }
                if (RemainingDebuffD > 0)
                {
                    DownD = "ダウンD";
                    RemainingDebuffD--;
                    if (RemainingDebuffD == 0)
                    {
                        DebuffD = 0;
                        DownD = "";
                        Console.WriteLine($"ダウンDが消えた、弱くなっていた防御が戻ってきた");
                        Thread.Sleep(500);
                    }
                }
                EnemyCold = false;


            }

        }
        static void PlayerTurn()
        {
            var ActionInput = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "attack", "defense", "magic"
            };
            Console.Write($"Player [HP: {PlayerHP}/{maxPlayerHP}] [MP: {PlayerMP}/{maxPlayerMP}]");
            if (Power != "")
            { Console.Write($"{Power}({RemainingPower}/3)"); }
            if (protect != "")
            { Console.Write($"{protect}({RemainingProtect}/3)"); }
            if (DownA != "")
            { Console.Write($" {DownA}({RemainingDebuffA}/3)"); }
            if (DownD != "")
            { Console.WriteLine($" {DownD}({RemainingDebuffD}/3)"); }
            Console.WriteLine();
            Console.WriteLine("[Attack] [Defense] [Item(未実装)] [Magic]");
            Console.WriteLine("あなたの行動");
            while (!ActionInput.Contains(Action))
            {
                if (Action == "debug")
                {
                    Console.WriteLine($"{EnemyHP} {PlayerDefense} {PlayerPower} {Defensetrue} {Protection}");
                }
                if (Action == "Item" || Action == "item")
                {
                    Console.WriteLine($"アイテムなんかねえよ、");
                    Thread.Sleep(1000);
                    Console.WriteLine($"うるせえよ、");
                    Thread.Sleep(1000);
                    Console.WriteLine($"黙れよ、");
                    Thread.Sleep(1000);
                    Console.WriteLine($"拳こそが正義");
                    Thread.Sleep(2000);
                    Console.WriteLine($"アイテムなんか、");
                    Thread.Sleep(1000);
                    Console.WriteLine($"ねえよ、");
                    Thread.Sleep(1000);
                    Console.WriteLine($"正しいのは俺");
                    Thread.Sleep(1000);
                    Console.WriteLine();
                }
                Console.Write("コマンド？");
                Action = Console.ReadLine() ?? "";
                if (Action == "help")
                { Help(); }
            }
            Console.WriteLine();
            if (Action == "Attack" || Action == "attack")
            {
                PlayerAttack();
            }
            else if (Action == "Defense" || Action == "defense")
            {
                Console.WriteLine("あなたは防御した");
                PlayerDefense += 50;
                Defensetrue = true;
                Thread.Sleep(500);
            }
            else if (Action == "Magic" || Action == "magic")
            {
                PlayerMagic();
            }
            magic = "";
            PlayerMP += 5;
            if (PlayerMP >= maxPlayerMP)
            {
                PlayerMP = maxPlayerMP;
            }
            if (PlayerHP > maxPlayerHP)
            { PlayerHP = maxPlayerHP; }
            Action = "";
            return;
        }
        static void PlayerAttack()
        {
            Random Attack = new Random();
            int Damage = (PlayerPower - DebuffA) + Attack.Next(((PlayerPower / 16) + 1) * -1, (PlayerPower / 16) + 1);
            Console.WriteLine("あなたの攻撃");
            Console.WriteLine();
            EnemyHP -= IntermediateDamage = Damage / 2 - Eenemydefense / 4;
            Console.WriteLine($"Enemyに {IntermediateDamage} のダメージ");
        }
        static void PlayerMagic()
        {
            string[] validmagic = { "パワー", "プロテクト", "ウォッシュ", "ヒール", "ヒールSP", "ファイア", "ファイアSP", "コールド", "コールドSP", "1", "2", "3", "4", "4SP", "5", "5SP", "6", "6SP" };
            Console.WriteLine($"  [(1)パワー] [(2)プロテクト] [(3)ウォッシュ]");
            Console.WriteLine($"[(4)ヒール(SP)] [(5)ファイア(SP)] [(6)コールド(SP)] ");
            while (!validmagic.Contains(magic))
            {
                magic = Console.ReadLine() ?? "";
            }
            if (magic == "パワー" || magic == "1" && PlayerMP > 14) //攻撃強化呪文処理
            {
                magic = "パワー";
                Console.WriteLine($"あなたは{magic}を唱えた");
                PlayerPower += 25;
                PlayerMP -= 20;
                if (RemainingPower == 0)
                {
                    RemainingPower = 4;
                }
                Thread.Sleep(500);
                Console.WriteLine($"力が強くなっている");
                Thread.Sleep(500);
            }
            else if (magic == "プロテクト" || magic == "2" && PlayerMP > 14) //防御呪文処理
            {
                magic = "プロテクト";
                Console.WriteLine($"あなたは{magic}を唱えた");
                PlayerMP -= 20;
                Protection = 50;
                RemainingProtect = 4;
                Thread.Sleep(500);
                Console.WriteLine($"守られている感覚がする");
            }
            else if (magic == "ウォッシュ" || magic == "3" && PlayerMP > 14) //デバフ解除処理
            {
                magic = "ウォッシュ";
                Console.WriteLine($"あなたは{magic}を唱えた");
                Thread.Sleep(500);
                if (RemainingDebuffA != 0 || RemainingDebuffD != 0)
                {
                    if (RemainingDebuffA <= 0)
                    {
                        RemainingDebuffA = 0;
                        DebuffA = 0;
                        DownA = "";
                        Console.WriteLine($"力が元に戻った");
                        Thread.Sleep(500);
                    }
                    if (RemainingDebuffD <= 0)
                    {
                        RemainingDebuffD = 0;
                        DebuffA = 0;
                        DownA = "";
                        Console.WriteLine($"防御が元に戻った");
                        Thread.Sleep(500);
                    }
                    PlayerMP -= 15;
                }
            }
            else if (magic == "ヒール" || magic == "4" && PlayerMP > 19) //回復呪文処理
            {
                magic = "ヒール";
                Random heel = new Random();
                Console.WriteLine($"あなたは{magic}を唱えた");
                Thread.Sleep(500);
                PlayerMP -= 20;
                int Heals = heel.Next(55, 66);
                PlayerHP += Heals;
                Console.WriteLine($"HPが{Heals}回復した");
                Thread.Sleep(500);
            }
            else if (magic == "ヒール" + "SP" || magic == "4" + "SP" && PlayerMP != 49) //特別回復呪文処理
            {
                int Heaslmiddle;
                magic = "ヒール";
                Random heel = new Random();
                Console.WriteLine($"あなたは{magic}スペシャルを唱えた");
                Thread.Sleep(500);
                int HealsSP = PlayerMP;
                PlayerHP += Heaslmiddle = HealsSP + 100;
                PlayerMP -= PlayerMP;
                Console.WriteLine($"HPが{Heaslmiddle}回復した");
                Thread.Sleep(500);
            }
            else if (magic == "ファイア" || magic == "5" && PlayerMP > 24) //炎の呪文処理
            {
                magic = "ファイア";
                Random MagicdamageFire = new Random();
                Console.WriteLine($"あなたは{magic}を唱えた");
                PlayerMP -= 25;
                int FireMagicDamage = PlayerMagicDamageRandom + MagicdamageFire.Next(((PlayerMagicDamageRandom / 16) + 1) * -1, (PlayerMagicDamageRandom / 16) + 1);
                EnemyHP -= IntermediateDamage = FireMagicDamage / 2 - Eenemydefense / 4;
                Console.WriteLine($"Enemyに{IntermediateDamage}のダメージ");
                Thread.Sleep(500);
            }
            else if (magic == "ファイア" + "SP" || magic == "5" + "SP" && PlayerMP > 74) //炎の呪文処理
            {
                magic = "ファイア";
                Random MagicdamageFire = new Random();
                Console.WriteLine($"あなたは{magic}スペシャルを唱えた");
                PlayerMP -= 75;
                for (int spFire = 1; spFire <= 3; spFire++)
                {
                    int FireMagicDamage = PlayerMagicDamageRandom + MagicdamageFire.Next(((PlayerMagicDamageRandom / 16) + 1) * -1, (PlayerMagicDamageRandom / 16) + 1);
                    EnemyHP -= IntermediateDamage = FireMagicDamage / 2 - Eenemydefense / 4;
                    Console.WriteLine($"Enemyに{IntermediateDamage}のダメージ");
                    Thread.Sleep(100);
                }

                Thread.Sleep(500);
            }
            else if (magic == "コールド" || magic == "6" && PlayerMP > 24) //氷の呪文処理
            {
                magic = "コールド";
                Random MagicdamageIce = new Random();
                Random Coldrnd = new Random();
                Console.WriteLine($"あなたは{magic}を唱えた");
                PlayerMP -= 25;
                int IceMagicDamage = PlayerMagicDamageRandom + MagicdamageIce.Next(((PlayerMagicDamageRandom / 20) + 1) * -1, (PlayerMagicDamageRandom / 20) + 1);
                int ColdEnemy = Coldrnd.Next(1, 101);
                EnemyHP -= IntermediateDamage = IceMagicDamage / 3 - Eenemydefense / 4;
                Console.WriteLine($"Enemyに{IntermediateDamage}のダメージ");
                Thread.Sleep(500);
                if (ColdEnemy <= 30)
                {
                    EnemyCold = true;
                    Console.WriteLine("Enemyは凍った");
                    Thread.Sleep(500);
                }
            }
            else if (magic == "コールド" + "SP" || magic == "6" + "SP" && PlayerMP >= 30) //氷の呪文処理
            {
                magic = "コールド";
                string? ColdCostMP = "";
                int SPCold = 71;
                Random MagicdamageIce = new Random();
                Random Coldrnd = new Random();
                
                while (SPCold > PlayerMP || SPCold > 70)
                {
                    ColdCostMP = Console.ReadLine() ?? "";
                    if (int.TryParse(ColdCostMP, out SPCold))
                    {
                        
                    }
                }
                Console.WriteLine($"あなたは{SPCold}MPを消費して{magic}スペシャルを唱えた");
                int IceMagicDamage = PlayerMagicDamageRandom + MagicdamageIce.Next(((PlayerMagicDamageRandom / 20) + 1) * -1, (PlayerMagicDamageRandom / 20) + 1);
                int ColdEnemy = Coldrnd.Next(1, 101);
                EnemyHP -= IntermediateDamage = IceMagicDamage / 3 - Eenemydefense / 4;
                Console.WriteLine($"Enemyに{IntermediateDamage}のダメージ");
                Thread.Sleep(500);
                if (ColdEnemy <= SPCold)
                {
                    EnemyCold = true;
                    Console.WriteLine("Enemyは凍った");
                    Thread.Sleep(500);
                }
                PlayerMP -= SPCold + 30;
            }
            else if (magic == "back")
            {
                Action = "";
                magic = "";
                PlayerTurn();
            }
        }
        static void Attack1()
        {
            Random AttackTyeprnd = new Random();
            int AttackTyep = AttackTyeprnd.Next(1, 5);
            Random EnemyPattern1 = new Random();

            if (AttackTyep == 1)
            {
                Random EnemySpecial = new Random();
                int EnemyAttackSP = EnemyPower + EnemySpecial.Next(((EnemyPower / 16) + 1) * -1, (PlayerPower / 16) + 1);
                Console.WriteLine("Enemyの痛恨の一撃");
                Thread.Sleep(500);
                PlayerHP -= IntermediateDamage = EnemyAttackSP + 50 / 2 - ((PlayerDefense - DebuffD) / 8 + Protection / 12);
                Thread.Sleep(500);
                Console.WriteLine($"あなたに{IntermediateDamage}のダメージ");
            }
            else
            {
                int EnemyAttack = EnemyPower + EnemyPattern1.Next(((EnemyPower / 16) + 1) * -1, (PlayerPower / 16) + 1);
                Console.WriteLine("Enemyの攻撃");
                Thread.Sleep(500);
                PlayerHP -= IntermediateDamage = EnemyAttack / 2 - ((PlayerDefense - DebuffD) / 4 + Protection / 6);
                Thread.Sleep(500);
                Console.WriteLine($"あなたに{IntermediateDamage}のダメージ");
                Thread.Sleep(500);
            }
            return;
        }
        static void Attack2()
        {
            Random DebuffRnd = new Random();
            Random NextEnemyPatternrnd = new Random();
            int Debuff = DebuffRnd.Next(1, 3);
            switch (Debuff)
            {
                case 1:
                    {
                        Console.WriteLine("EnemyはダウンAを唱えた");
                        DebuffA = 30;
                        DownA = "ダウンA";
                        Console.WriteLine("力が弱まった");
                        if (RemainingDebuffA == 0)
                        {
                            RemainingDebuffA = 4;
                        }
                        Thread.Sleep(500);
                        break;
                    }
                case 2:
                    {
                        Console.WriteLine("EnemyはダウンDを唱えた");
                        Thread.Sleep(500);
                        DebuffD = 30;
                        DownD = "ダウンD";
                        Thread.Sleep(500);
                        Console.WriteLine("防御が弱くなった");
                        if (RemainingDebuffD == 0)
                        {
                            RemainingDebuffD = 4;
                        }
                        Thread.Sleep(500);
                        break;
                    }
                
            }
            if (Enemy2actions)
            {
                int NextEnemyPattern = NextEnemyPatternrnd.Next(1, 4);
                if (NextEnemyPattern == 1)
                {
                    Attack1();
                }
                else
                {
                    return;
                }
            }
            
        }
        static void Attack3()
        {

            if (RemainingPower != 0 || RemainingProtect != 0)
            {
                Console.WriteLine("Enemyの手から波動がが迸る");
                if (RemainingPower != 0)
                {
                    RemainingPower = 1;
                }
                if (RemainingProtect != 0)
                {
                    RemainingProtect = 1;
                }

                Thread.Sleep(500);
            }
            else
            {
                Console.WriteLine("Enemyは不敵な笑みを浮かべている");
                Thread.Sleep(500);
            }

        }
        static void EnemySpecialAttack()
        {

        }
        static void Help()
        {
            Console.WriteLine("ゲームヘルプ");
            Console.WriteLine("このゲームについて : このゲームはテキストオンリーのコマンドバトルです、敵か自身のHPが0になると終了します");
            Console.WriteLine("行動 : [Attack] [Defense] [Magic]と入力することでそれぞれの行動を実行できます");
            Console.WriteLine("Attack : 敵に攻撃する基本の行動です");
            Console.WriteLine("Defense : 敵からの攻撃を防御する行動です");
            Console.WriteLine("Magic : 複数の魔法から1つを選択しMPを消費し使用できます、一部の呪文は末尾にSPと入力することで消費するMPと効果を増加させます");
            Console.WriteLine("各種魔法 : 魔法の中から名前、または割り当てられた数字を入力することで使用できます");
            Console.WriteLine("　パワー 20MP : 攻撃力を3ターンの間上昇させます、複数回使用することで攻撃力がさらに上昇しますがターンは増加せずそのままです");
            Console.WriteLine("　プロテクト 20MP : 防御力を3ターンの間上昇させます、複数回使用しても防御力がさらに上昇することはありませんがターンは増加します");
            Console.WriteLine("　ウォッシュ 15MP : 自身の能力を低下する状態を消します");
            Console.WriteLine("　ヒール 20MP/50~MP : 自身のHPを60程回復します、スペシャル状態は50MP以上で使用でき全てのMPを消費して100+消費した分のMP回復します");
            Console.WriteLine("　ファイア 25MP/75MP : 敵に強力な攻撃を与えます、スペシャル状態では75MPを使用して3回攻撃を与えます");
            Console.WriteLine("　コールド 25MP/~70MP : 敵単体に攻撃を与え確率で行動不能状態にします、スペシャル状態では任意の値(最大70MP)のMPを消費して行動不能にする確率を上昇させます");
            Console.WriteLine(" backと入力するとコマンド選択に戻れます");
            Console.WriteLine("アイテム : 未実装です");
        }
        static void End()
        { Console.ReadLine(); }
    }
}
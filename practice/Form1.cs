using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace practice
{
    public partial class Form1 : Form
    {
        public string account;
        public string trnPassword;
        public int[] password;
        public int logincount;
        public int accountlength;
        public int passwordlength;
        AccountClass accountClass = new AccountClass();
        public Dictionary<string, string> MainDic = new Dictionary<string, string>();//主要字典
        public Dictionary<string, AccountClass> AccountDic = new Dictionary<string, AccountClass>();//帳號字典

        public Form1()
        {
            InitializeComponent();
            label1.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            do
            {
                account = getAccount();//取得帳號

            } while (MainDic.ContainsKey(account));//if (MainDic.ContainsKey(account) == true)(MainDic前面加驚嘆號意思相反)
            int[] password = getPassword();//取得密碼
            string trnPassword = translatePassword(password);
            MainDic = accountpassword(account, trnPassword, MainDic);//將產生的帳號密碼存入字典
            AccountDic.Add(account, accountClass);
            label1.Text = account + "\n" + BindUser(password);//顯示帳號密碼
            //label1.Text += BindUser(account,password);//顯示帳號密碼
        }
        private string getAccount()//取得指定長度帳號
        {
            int.TryParse(textBox4.Text, out accountlength);
            Random account;
            if (accountlength > 9) accountlength = 9;
            var accountbox = "qwertyuiopasdfghjklzxcvbnm";
            string user = "";
            account = new Random();
            for (int i = 0; i < accountlength; i++)//取得隨機accountbox內字母accountlength次
            {
                user += accountbox[account.Next(accountbox.Length)];
            }
            return user;
        }

        private int[] getPassword()
        {
            int.TryParse(textBox3.Text, out passwordlength);
            if (passwordlength > 9) passwordlength = 9;
            int[] password = new int[passwordlength];
            Random getPassword = new Random();//產生隨機4個數字
            for (int i = 0; i < passwordlength; i++)
            {
                password[i] = getPassword.Next(0, 9);
            }
            return password;
        }



        private void button2_Click(object sender, EventArgs e)
        {
            string userAccount = textBox1.Text;
            string userPasswod = textBox2.Text;
            if (accountClass.loginCount == 3)
            {
                accountClass.loginFail = true;
            }
            if (accountClass.loginFail == true)
            {
                label2.Text = "returned";
                return;
            }
            if (MainDic.TryGetValue(userAccount, out trnPassword))
            {
                if (trnPassword == userPasswod)
                {
                    label2.Text = "log in sussess";
                }
                else
                {
                    label2.Text = "log in failed";
                    accountClass.loginCount++;
                }
            }
            else
            {
                label2.Text = "Wrong account";
                accountClass.loginCount++;
            }
        }

        private Dictionary<string, string> accountpassword(string key, string value, Dictionary<string, string> mainDic)//次要字典
        {
            mainDic.Add(key, value.ToString());
            return mainDic;
        }
        public string BindUser(int[] password)
        {
            string user = "";
            foreach (int i in password)
            {
                label1.Text += i.ToString();
                user += i.ToString();
            }
            //string user = password.ToString();
            return user;
        }
        public string translatePassword(int[] password)
        {
            string user = "";
            foreach (int i in password)
            {
                user += i.ToString();
            }
            return user;
        }
    }
}

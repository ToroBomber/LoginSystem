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
        //public Dictionary<string, string> MainDic = new Dictionary<string, string>();//主要字典
        public Dictionary<string, AccountClass> AccountDic = new Dictionary<string, AccountClass>();//帳號字典

        public Form1()
        {
            InitializeComponent();
            label1.Text = "";
        }
        /// <summary>
        /// 當創造帳號按鈕按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            do
            {
                account = getAccount();//取得帳號

            } while (AccountDic.ContainsKey(account));//if (MainDic.ContainsKey(account) == true)(MainDic前面加驚嘆號意思相反)
            int[] password = getPassword();//取得密碼
            string trnPassword = translatePassword(password);
            accountClass.password = trnPassword;
            accountClass.account = account;
            //AccountDic = accountpassword(account,accountClass);
            AccountDic.Add(account, accountClass);//將產生的帳號密碼存入字典
            label1.Text = account + "\n" + BindUser(password);//顯示帳號密碼
            //label1.Text += BindUser(account,password);//顯示帳號密碼
        }
        /// <summary>
        /// 帳號產生
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// 密碼產生
        /// </summary>
        /// <returns></returns>
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
            label2.Text = "";
            if (AccountDic[userAccount].loginCount == 3)//檢查該帳號登入失敗次數
            {
                AccountDic[userAccount].loginFail = true;
            }
            if (AccountDic[userAccount].loginFail == true)//如果登入失敗3次就不給登
            {
                label2.Text = "returned";
                return;
            }
            if (AccountDic.TryGetValue(userAccount, out accountClass))//檢查帳號是否存在(是)
            {
                if (AccountDic[userAccount].password == userPasswod)//檢查帳號密碼是否正確
                {
                    label2.Text = "log in sussess";
                }
                else
                { 
                    label2.Text = "log in failed";
                    AccountDic[userAccount].loginCount++;
                }
            }
            else//檢查帳號是否存在(否)
            {
                label2.Text = "Wrong account";
            }
        }

        //private Dictionary<string, AccountClass> accountpassword(string key,Dictionary<string, AccountClass> mainDic)//次要字典
        //{
        //    mainDic.Add(key, accountClass);
        //    return mainDic;
        //}
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

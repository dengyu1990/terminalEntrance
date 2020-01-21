using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Runtime.InteropServices;
using System.DirectoryServices;


namespace verifyTest
{
    class Program
    {

        public static Dictionary<string, string> opeData = new Dictionary<string, string>();
        static void Main(string[] args)
        {           
            opeData.Add("10.11.75.21", "CPU 100% RAM 88%");
            opeData.Add("10.11.75.22", "CPU 98% RAM 90%");
            opeData.Add("10.15.37.126", "CPU 3% RAM 23%");

            foreach (string ip in opeData.Keys)
                Console.WriteLine(ip + opeData[ip]);
            Console.WriteLine("---------------------------------");

            Change(new Dictionary<string, string>(opeData));

            Console.WriteLine("---------------------------------");
            foreach (string ip in opeData.Keys)
                Console.WriteLine(ip + opeData[ip]);
            Console.ReadKey();
        }

        static void Change(Dictionary<string, string> tmps)
        {
            tmps.Remove("10.5.65.25");
            foreach (string ip in tmps.Keys)
                Console.WriteLine(ip + tmps[ip]);
        }

    }
}


//C# Research Notes
/*string domainIP = "10.15.33.12";
Console.WriteLine("域服务验证模块（测试），请输入域帐号：");
string userAccount = Console.ReadLine();
Console.WriteLine("请输入密码：");
string Password = Console.ReadLine();

using (DirectoryEntry deUser = new DirectoryEntry(@"LDAP://" + domainIP, userAccount, Password))
{
    DirectorySearcher src = new DirectorySearcher(deUser);
    src.Filter = "(&(&(objectCategory=person)(objectClass=user))(sAMAccountName=" + userAccount + "))";
    src.PropertiesToLoad.Add("cn");
    src.SearchRoot = deUser;
    src.SearchScope = SearchScope.Subtree;
    SearchResult result;
    try
    {
        result = src.FindOne();
    }
    catch
    {
        Console.WriteLine("不存在此用户");
        Console.ReadKey();
        return;
    }
    if (result != null)
    {
        DirectoryEntry de = result.GetDirectoryEntry();
        string userID = de.Username;
        Console.WriteLine("验证成功:" + userID);
    }


}
Console.ReadKey();
*/

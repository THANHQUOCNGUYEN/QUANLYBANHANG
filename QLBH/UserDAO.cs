using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Security;
using QLBH.Models;
namespace QLBH
{
    public class UserDAO
    {
        public QuanLyBanHang db = new QuanLyBanHang();
        public string MD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }

        public bool checkemail(string email)    
        {
            return db.Users.Count(s => s.Email == email) > 0;
        }
       
        public bool checktendangnhap(string tendangnhap)
        {
            return db.Users.Count(s => s.UserName == tendangnhap) > 0;
        }

    }
}
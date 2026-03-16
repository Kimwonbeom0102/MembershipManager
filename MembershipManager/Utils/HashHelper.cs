using System.Security.Cryptography;
using System.Text;

namespace MembershipManager.Utils
{
    public static class HashHelper
    {
        /// <summary>
        /// 문자열을 SHA256 해시로 변환
        /// </summary>
        public static string ComputeSha256(string rawData)
        {
            // SHA256 객체 생성
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // 문자열 → 바이트 변환
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                StringBuilder builder = new StringBuilder();

                // 바이트 → 16진수 문자열 변환
                foreach (byte t in bytes)
                {
                    builder.Append(t.ToString("x2"));
                }

                return builder.ToString();
            }
        }
    }
}
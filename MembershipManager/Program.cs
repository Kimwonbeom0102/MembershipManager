using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MembershipManager.Services;

namespace MembershipManager
{
    static class Program
    {
        /// <summary>
        /// 해당 응용 프로그램의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            MemberService service = new MemberService();

            // 로그인 폼 실행
            LoginForm loginForm = new LoginForm(service);
            
            if(loginForm.ShowDialog() == DialogResult.OK)
            {
                var member = loginForm.LoggedInMember;

                // 관리자 로그인
                if (member.Role != null && member.Role == "Admin")
                {
                    // 메인 폼 실행
                    Application.Run(new MainForm(service));
                }
                else
                {
                    // 일반 사용자
                    MessageBox.Show("일반 사용자 기능은 준비중입니다.");
                }
            }

            //if(loginForm.ShowDialog() == DialogResult.OK)
            //{
            //    Application.Run(new LoginForm(service));
            //}
        }
    }
}

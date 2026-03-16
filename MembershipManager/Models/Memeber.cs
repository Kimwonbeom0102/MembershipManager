using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MembershipManager.Models
{
    /// <summary>
    /// Member 클래스
    /// 
    /// 회원 한 명의 정보를 저장하는 데이터 모델
    /// 왜 모델을 따로 만들까?
    /// UI(TextBox 등)와 실제 데이터를 분리
    /// 실무에서는 데이터 구조(Model)를 먼저 정의
    /// </summary>
    public class Member
    {
        // 회원 고유 번호
        // 프로그램 내부에서 회원을 구분하기 위한 값
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public string Phone { get; set; }
        
        public string Address { get; set; }
        
        public string Username { get; set; }
        
        public string Password { get; set; }

        // 추가 (관리자, 일반사용자 접근 구분)
        public string Role { get; set; }
    }
}

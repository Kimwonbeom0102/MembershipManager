using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MembershipManager.Models;

// JSON / DB 사용하기 이전 List<Member>로
// 회원을 관리하기 위한 용도
// ** JSON으로 변경하여 사용하지않음 **
namespace MembershipManager.Data
{
    /// <summary>
    /// MemberStorage
    /// 
    /// 실제 데이터 저장소 역할
    /// 
    /// 기존 JSON / DB 대신
    /// List<Member> 사용 -> 변경 완료
    /// 
    /// - CRUD 로직 학습이 목적
    /// - 저장 시스템을 단순화
    /// 
    /// JSON / DB로 변경
    /// </summary>
    public class MemberStorage
    {
        // 메모리 저장소
        // 프로그램이 실행되는 동안만 데이터 유지
        private List<Member> members = new List<Member>();

        /// <summary>
        /// 전체 회원 목록 반환
        /// </summary>
        public List<Member> GetMembers()
        {
            return members;
        }
    }
}

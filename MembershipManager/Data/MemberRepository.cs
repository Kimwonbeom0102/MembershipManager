using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using MembershipManager.Models;

namespace MembershipManager.Data
{
    /// <summary>
    /// Repository 계층
    /// JSON 파일 저장 / 로드 담당
    /// </summary>
    public class MemberRepository
    {
        // JSON 파일 경로
        private string filePath = "members.json";

        /// <summary>
        /// JSON 파일에서 회원 목록 불러오기
        /// </summary>
        public List<Member> LoadMembers()
        {
            // 파일이 존재하지 않으면
            if (!File.Exists(filePath))
            {
                // 빈 리스트 반환
                return new List<Member>();
            }

            // JSON 파일 읽기
            string json = File.ReadAllText(filePath);

            // JSON → List<Member> 변환
            return JsonConvert.DeserializeObject<List<Member>>(json); // 역직렬화
        }

        /// <summary>
        /// JSON 파일 저장
        /// </summary>
        public void SaveMembers(List<Member> members)
        {
            // List<Member> → JSON 문자열 변환
            string json = JsonConvert.SerializeObject(members, Formatting.Indented);

            // JSON 파일 저장
            File.WriteAllText(filePath, json);
        }
    }
}
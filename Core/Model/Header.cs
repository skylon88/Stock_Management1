using System;

namespace Core.Model
{
    public class Header
    {
        public int SerialNo { get; set; } //递增序号
        public string ApplicationPerson { get; set; } //需求申请人
        public string ApplicationDept { get; set; } //需求申请部门
        public string CreatePerson { get; set; } //制表人
        public string FollowupPerson { get; set; } //物管跟进人
        public string Auditor { get; set; } //审核人
        public string AuditDepart { get; set; } //审核部
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime UpdateDate { get; set; } = DateTime.Now;
    }
}

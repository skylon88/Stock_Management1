using System;

namespace Core.Model
{
    public class FixModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Address { get; set; } //维修地址
        public string Contact { get; set; } //维修联系人
        public string Phone { get; set; } //维修联系人电话
        public double Price { get; set; } //维修金额
        public int Days { get; set; } //维修天数
        public DateTime? FinishDate { get; set; } //取货日期
    }
}

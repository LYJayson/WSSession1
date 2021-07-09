namespace WSSession2
{
    class Session2
    {
        /// <summary>
        /// 计划名称
        /// </summary>
        private static string Plan_Name;
        public static string Plan_name { get => Plan_Name; set => Plan_Name = value; }

        /// <summary>
        /// 计划Id
        /// </summary>
        private static string Plan_Id;
        public static string Plan_id { get => Plan_Id; set => Plan_Id = value; }

        /// <summary>
        /// 视频判定变量(绑定视频 || 已绑定视频)
        /// </summary>
        private static string ACtion;
        public static string Action { get => ACtion; set => ACtion = value; }

        /// <summary>
        /// 视频类型
        /// </summary>
        private static string Moive_Type;
        public static string Moive_type { get => Moive_Type; set => Moive_Type = value; }

        /// <summary>
        /// 视频名称 (*.*)
        /// </summary>
        private static string Moive_Name;
        public static string Moive_name { get => Moive_Name; set => Moive_Name = value; }
    }
}

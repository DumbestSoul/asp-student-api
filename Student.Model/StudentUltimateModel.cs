namespace Student.Model
{
	public class StudentUltimateModel
	{
        public int StudentId { get; set; }
		public string Name { get; set; }
		public double TotalMark { get; set; } = 0.0;
		public double TotalMarkObtained { get; set; } = 0.0;
		public double TotalPercentage { get; set; } = 0.0;
    }
}

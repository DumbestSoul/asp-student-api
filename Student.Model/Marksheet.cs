using Student.Enums;

namespace Student.Model
{
	public class Marksheet
	{
        public int MarksheetId { get; set; }
        public int StudentId { get; set; }
        public Subjects Subject { get; set; }   // ENUM TYPE
        public double TotalMark { get; set; }
        public double MarksObtained { get; set; }

		public override string ToString() => "<" + MarksheetId + "," + StudentId +  "," + Subject + ">";
		
	}
}

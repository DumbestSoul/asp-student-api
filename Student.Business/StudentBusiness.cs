using Student.Model;
using Student.Enums;
using System.Runtime.InteropServices;

namespace Student.Business
{
	public static class StudentBusiness
	{
		private static List<StudentModel> Students;
		private static List<Marksheet> Marksheets;
		private static int s_id = 6;
		private static int m_id = 10;
		static StudentBusiness()
		{
			/*****************************************************************
										INITIALISATION
			 ****************************************************************/
			Students = new List<StudentModel>
			{
				new StudentModel{StudentId=1, Name="Luffy", JoinDate=new DateTime(2024, 04, 29), Standard=12},
				new StudentModel{StudentId=2, Name="Zoro", JoinDate=new DateTime(2024, 05, 28), Standard=11},
				new StudentModel{StudentId=3, Name="Sanji", JoinDate=new DateTime(2024, 03, 11), Standard=11},
				new StudentModel{StudentId=4, Name="Nami", JoinDate=new DateTime(2024, 10, 15), Standard=12},
				new StudentModel{StudentId=5, Name="Ussop", JoinDate=new DateTime(2024, 11, 9), Standard=10}
			};

			Marksheets = new List<Marksheet>
			{
				new Marksheet{MarksheetId=1, StudentId=1, Subject=Subjects.PHYSICS, TotalMark=100, MarksObtained=34},
				new Marksheet{MarksheetId=2, StudentId=1, Subject=Subjects.CHEMISTRY, TotalMark=100, MarksObtained=35},
				new Marksheet{MarksheetId=3, StudentId=2, Subject=Subjects.ENGLISH, TotalMark=100, MarksObtained=78},
				new Marksheet{MarksheetId=4, StudentId=3, Subject=Subjects.BIOLOGY, TotalMark=100, MarksObtained=86},
				new Marksheet{MarksheetId=5, StudentId=5, Subject=Subjects.COMPUTER, TotalMark=100, MarksObtained=94},
				new Marksheet{MarksheetId=6, StudentId=2, Subject=Subjects.PHYSICS, TotalMark=100, MarksObtained=33},
				new Marksheet{MarksheetId=7, StudentId=4, Subject=Subjects.PHYSICS, TotalMark=100, MarksObtained=96},
				new Marksheet{MarksheetId=8, StudentId=4, Subject=Subjects.MATHS, TotalMark=100, MarksObtained=98},
				new Marksheet{MarksheetId=9, StudentId=3, Subject=Subjects.CHEMISTRY, TotalMark=100, MarksObtained=87},
			};
		}

		public static void GetList()
		{
			foreach(Marksheet mrk in Marksheets)
			{
				Console.WriteLine(mrk.ToString());
			}
		}

		/*************************************************************
		 *					GET METHOD BUSINESS						 *						
		 ************************************************************/
		public static List<StudentModel> GetAllStudents() => Students;
		public static List<Marksheet> GetMarksheets() => Marksheets;

		public static double GetTotalMarkObtained(int id)
		{
			List<Marksheet> marksheets = Marksheets.FindAll(m => m.StudentId == id);
			if (marksheets.Count == 0) return -1;

			double res = 0.0;
			foreach(Marksheet mrks in marksheets)
			{
				res += mrks.MarksObtained;
			}
			return res;
		}

		public static List<SubjectAndMarksModel>? GetAllMarksById(int id)
		{
			List<Marksheet> mrks = Marksheets.FindAll(e => e.StudentId == id);
			if (mrks.Count == 0) return null;
			List<SubjectAndMarksModel> res = new List<SubjectAndMarksModel>();
			foreach(Marksheet mrk in mrks)
			{
				res.Add(new() { Subject=mrk.Subject, Marks=mrk.MarksObtained});
			}
			return res;
		}

		public static SubjectPercentageModel? GetTotalPercentageObtained(int id)
		{
			List<Marksheet> mrks = Marksheets.FindAll(e => e.StudentId == id);
			if (mrks.Count == 0) return null;
			Console.WriteLine(mrks);
			SubjectPercentageModel spm = new SubjectPercentageModel();
			foreach(Marksheet mrk in mrks)
			{
				double percentage = (mrk.MarksObtained / mrk.TotalMark) * 100;
				if (mrk.Subject == Subjects.MATHS)
				{
					spm.MathsPercentage = percentage;
				}
				if (mrk.Subject == Subjects.PHYSICS)
				{
					spm.PhysicsPercentage = percentage;
				}
				if (mrk.Subject == Subjects.CHEMISTRY)
				{
					spm.ChemistryPercentage = percentage;
				}
				if (mrk.Subject == Subjects.BIOLOGY)
				{
					spm.BiologyPercentage = percentage;
				}
				if (mrk.Subject == Subjects.ENGLISH)
				{
					spm.EnglishPercentage = percentage;
				}
				if (mrk.Subject == Subjects.COMPUTER)
				{
					spm.ComputerPercentage = percentage;
				}
			}
			return spm;
		}

		public static List<StudentUltimateModel> GetStudentList()
		{
			List<StudentUltimateModel> res = new List<StudentUltimateModel>();
			foreach(StudentModel student in Students)
			{
				List<Marksheet> mrks = Marksheets.FindAll(m => m.StudentId == student.StudentId);
				if (mrks.Count == 0)
				{
					res.Add(new() {StudentId=student.StudentId, Name=student.Name});
					continue;
				}
				double TotalMarks=0, TotalMarksObtained=0, TotalPercentage = 0;
				foreach(Marksheet mrk in mrks)
				{
					TotalMarks += mrk.TotalMark;
					TotalMarksObtained += mrk.MarksObtained;

				}
				TotalPercentage = (TotalMarksObtained / TotalMarks) * 100;
				res.Add(new() { StudentId = student.StudentId, Name = student.Name, TotalMark = TotalMarks, TotalMarkObtained = TotalMarksObtained, TotalPercentage = TotalPercentage });
			}
			return res;
		}

		/*************************************************************
		 *					POST METHOD METHOD						 *
		 ************************************************************/
		public static bool AddMarks(Marksheet m)
		{
			if (Students.Find(p => p.StudentId == m.StudentId) == null) return false;  // student does not exists
																		//if the student exists
			if (Marksheets.Find(p => (p.StudentId == m.StudentId && p.Subject == m.Subject)) != null) return false; //the suvject entry already exists so return
			
			m.MarksheetId = m_id++;
			Marksheets.Add(m);
			return true;
		}

		/************************************************************
		 *					PUT METHOD BUSINESS						*
		 ***********************************************************/
		public static bool UpdateMarks(Marksheet m)
		{
			var i = Marksheets.FindIndex(p => p.StudentId == m.StudentId && p.Subject == m.Subject);
			if (i == -1) return false;
			//else case
			Marksheets[i] = m;
			return true;
		}
	}
}

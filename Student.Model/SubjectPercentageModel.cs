using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student.Model
{
	public class SubjectPercentageModel
	{
        public int StudentId { get; set; }
		public double MathsPercentage { get; set; } = 0.0;
		public double PhysicsPercentage { get; set; } = 0.0;
		public double ChemistryPercentage { get; set; } = 0.0;
		public double BiologyPercentage { get; set; } = 0.0;
		public double EnglishPercentage { get; set; } = 0.0;
		public double ComputerPercentage { get; set; } = 0.0;
	}
}

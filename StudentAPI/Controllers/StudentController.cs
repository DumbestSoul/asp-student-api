using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Student.Business;
using Student.Enums;
using Student.Model;

namespace StudentAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class StudentController : ControllerBase
	{
		/**************************************************************************
		 *																		  *
		 *								GET METHODS								  *
		 *																		  *																		  *
		 *************************************************************************/

		[HttpGet("Students")]
		public ActionResult<List<StudentModel>> GetStudents() => StudentBusiness.GetAllStudents();

		[HttpGet("Marksheets")]
		public ActionResult<List<Marksheet>> GetMarksheets() => StudentBusiness.GetMarksheets();

		[HttpGet("Marksheets/GetTotalMarksObtained/{id}")]
		public ActionResult<double> GetTotalMarksObtained(int id)
		{
			var res = StudentBusiness.GetTotalMarkObtained(id);
			if (res == -1) return NotFound();
			return res;
		}

		[HttpGet("Marksheets/GetAllMarksById/{id}")]
		public ActionResult<List<SubjectAndMarksModel>> GetAllMarksById(int id)
		{
			var res = StudentBusiness.GetAllMarksById(id);
			if (res == null) return NotFound();
			return res;
		}

		[HttpGet("Marksheets/GetTotalPercentageObtained/{id}")]
		public ActionResult<SubjectPercentageModel> GetTotalPercentageObtained(int id)
		{
			var res = StudentBusiness.GetTotalPercentageObtained(id);
			if (res == null) return BadRequest();
			return res;
		}

		[HttpGet("Marksheets/GetStudentList")]
		public ActionResult<List<StudentUltimateModel>> GetStudentList()
		{
			return StudentBusiness.GetStudentList();	
		}

		/**************************************************************************
		 *																		  *
		 *							POST METHODS								  *
		 *																		  *																		  *
		 *************************************************************************/
		[HttpPost("AddMarks")]
		public ActionResult AddMarks(Marksheet m)
		{
			bool res = StudentBusiness.AddMarks(m);
			if (!res) return BadRequest();

			return CreatedAtAction("AddMarks", m);
		}

		[HttpPut("UpdateMarks")]
		public ActionResult UpdateMarks(Marksheet m)
		{
			bool res = StudentBusiness.UpdateMarks(m);
			if (!res) return NoContent();

			return Ok("Content Update");
		}
	}
}

using nomina2.Models.DAO;
using nomina2.Models.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

public class RecordController : Controller
{
    private RecordDAO recordRepository = new RecordDAO();

    public ActionResult Calendar()
    {
        List<RecordDTO> allRecords = recordRepository.ReadAllRecords();
        List<RecordDTO> acceptedRecords = allRecords.Where(r => r.Active == 2).ToList();

        return View(acceptedRecords);
    }

    public ActionResult Historical(int userId)
    {
        List<RecordDTO> userRecords = recordRepository.ReadRecordsByUserId(userId);

        return View(userRecords);
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TestWebApi.Models;

namespace TestWebApi.Controllers
{
    [Route("api/infinilist")]
    [ApiController]
    public class InfinilistController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly SqlConnection myCon;

        public InfinilistController(IConfiguration configuration) {
            _configuration = configuration;
            myCon = new SqlConnection(_configuration.GetConnectionString("infinilistDB"));
        }

        [HttpPost]
        [Route("create")]
        public JsonResult Create(Data_CreateModel model) {
            SqlService ss = new SqlService("[dbo].[Data_Create]", myCon);
            SqlParam[] ps = new SqlParam[3];
            ps[0] = (ss.NewParam("@data", SqlDbType.VarChar, model.data));
            ps[1] = (ss.NewParam("@type", SqlDbType.VarChar, model.type));
            ps[2] = (ss.NewParam("@parentId", SqlDbType.BigInt, model.parentId));
            return ss.Execute(ps, true, myCon);
        }


        [HttpGet]
        [Route("readtypeanddata")]
        public JsonResult Read(){
            SqlService ss = new SqlService("[dbo].[Data_ReadTypeAndData]", myCon);
            QueryStringService qss = new QueryStringService(Request.QueryString.Value);
            SqlParam[] ps = new SqlParam[2];
            ps[0] = (ss.NewParam("@type", SqlDbType.VarChar, qss.getData("type")));
            ps[1] = (ss.NewParam("@data", SqlDbType.VarChar, qss.getData("data")));
            return ss.Execute(ps, true, myCon);
        }

        [HttpGet]
        [Route("readrange")]
        public JsonResult ReadByParent() {
            SqlService ss = new SqlService("[dbo].[Data_ReadRange]", myCon);
            QueryStringService qss = new QueryStringService(Request.QueryString.Value);
            SqlParam[] ps = new SqlParam[3];
            ps[0] = (ss.NewParam("@id", SqlDbType.BigInt, Int32.Parse(qss.getData("id"))));
            ps[1] = (ss.NewParam("@min", SqlDbType.BigInt, Int32.Parse(qss.getData("min"))));
            ps[2] = (ss.NewParam("@max", SqlDbType.BigInt, Int32.Parse(qss.getData("max"))));
            return ss.Execute(ps, true, myCon);
        }

        [HttpPut]
        [Route("updatebyid")]
        public JsonResult Update(Data_UpdateModel model){
            SqlService ss = new SqlService("[dbo].[Data_Update]", myCon);        
            SqlParam[] ps = new SqlParam[4];
            ps[0] = (ss.NewParam("@id", SqlDbType.BigInt, model.id));
            ps[1] = (ss.NewParam("@data", SqlDbType.VarChar, model.data));
            ps[2] = (ss.NewParam("@type", SqlDbType.VarChar, model.type));
            ps[3] = (ss.NewParam("@parentId", SqlDbType.BigInt, model.parentId));
            return ss.Execute(ps, true, myCon);
        }

        [HttpDelete]
        [Route("deletebyparentid")]
        public JsonResult DeleteByParent(Data_DeleteByParentModel model) {
            SqlService ss = new SqlService("[dbo].[Data_DeleteChilds]", myCon);
            SqlParam[] ps = new SqlParam[2];
            ps[0] = (ss.NewParam("@parentId", SqlDbType.BigInt, model.id));
            ps[1] = (ss.NewParam("@deleteParent", SqlDbType.Bit, model.deleteParent));
            return ss.Execute(ps, false, myCon);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Calc.Models;
using System.Data;


namespace Calc.Controllers
{
    public class ValuesController : ApiController
    {
        CalcContext db = new CalcContext();
        // GET api/values
        public IEnumerable<Calculating> GetCalculatings()
        {
            return db.Calcs;
        }

        // GET api/values/5
        public Calculating GetCalc(int id)
        {
            Calculating calc = db.Calcs.Find(id);
            return calc;
        }

        [HttpPost]
        public void AddCalc([FromBody]Calculating calcdate)
        {
            if(!String.IsNullOrEmpty(calcdate.Term))
            {
                string s = calcdate.Term;
                int iCntOprnd = 0, iCntOprt = 0;
                bool bflag = true;
                for (int i = 0; i < s.Length; i++)
                {
                    if ((s[i] == '+') || (s[i] == '-') || (s[i] == '*') || (s[i] == '/'))
                    {
                        if (iCntOprt >= 1)
                        {
                            calcdate.Operations += ";";
                        }
                        calcdate.Operations += s[i];                        
                        iCntOprt += 1;
                        calcdate.Operands += ";";
                        if(bflag)
                            iCntOprnd += 1;
                        bflag = false;                    
                            
                    }
                    else
                    {
                        calcdate.Operands += s[i];
                        bflag = true;
                        if(i == s.Length -1)
                        {
                            calcdate.Operands += ";";
                            iCntOprnd += 1;
                        }

                    }
                        
                }
                if(iCntOprnd - iCntOprt == 1)
                {
                    PostfixNotationExpression pn = new PostfixNotationExpression();
                    calcdate.Res = pn.result(s);


                    calcdate.TimeofCalc = DateTime.Now;
                    calcdate.UserIp = System.Web.HttpContext.Current.Request.UserHostAddress;
                    db.Calcs.Add(calcdate);
                    db.SaveChanges();

                }


            }
            
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

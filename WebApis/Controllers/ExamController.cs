using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System;
using WebApis.Models;

namespace WebApis.Controllers
{
    public class ExamController : Controller
    {
        [HttpGet]
        [Route("Permutations")]
        public ActionResult Permutations(string input = "abc")
        {
            List<string> lstResult = new();

            var len = input.Length;
            if (len == 1)
                lstResult.Add(input);

            for (int i = 0; i < len; i++)
            {
                string diff = input.Remove(i, 1);

                for (int j = 0; j < diff.Length; j++)
                {
                    string str = input[i].ToString();
                    string digit = diff[j].ToString();
                    string diff2 = diff.Remove(j, 1);

                    str += digit + diff2;
                    lstResult.Add(str);
                }

            }


            return Ok(lstResult);
        }

        [HttpGet]
        [Route("FindTheOddInt")]
        public ActionResult FindTheOddInt(int[] input)
        {
            int result = 0;
            var dict = new Dictionary<int, int>();

            foreach (var value in input)
            {
                dict.TryGetValue(value, out int count);
                dict[value] = count + 1;
            }

            foreach (var pair in dict) {
                if (pair.Value % 2 == 1)
                    result = pair.Key;
            }

            return Ok(result);
        }


        [HttpGet]
        [Route("CountFaceSmile")]
        public ActionResult CountFaceSmile(string[] smiles)
        {
            int result = 0;
            string validSmile = ":) :D ;-D :~";

            foreach (var smile in smiles) 
            {
                bool is_smile = validSmile.Contains(smile);
                if (is_smile)
                    result++;
            }

            return Ok(result);
        }

    }
}

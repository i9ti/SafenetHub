using System;
using System.Linq;
using System.Text.Json;
using System.IO;

namespace SafnetHub
{
    class ConvertJson
    {
        static void Main(string[] args)
        {
            try
            {
                var lines = File.ReadAllLines(@"file\" + args[0]);
                Console.WriteLine("Lendo arquivo ...\n{0}", lines);

                string[] lines_convert = new string[lines.Count()];
                int i = 0;
                foreach (string line in lines)
                {
                    lines_convert[i] = Convert.ToString(line).Replace(" ", "").Replace("(", "|").Replace(")", "").Replace("|N", "|N|").Replace("|A", "|A|");
                    i++;
                }

                string str_parameters = "";
                string size = "0";
                int ocurrs = 0;

                foreach (string str_params in lines_convert)
                {
                    string ocorrencias = Convert.ToString(str_params.Split("|")[2]);
                    if (ocorrencias.Contains("/")){
                        size   = Convert.ToString(ocorrencias.Split("/")[0]);
                        ocurrs = Convert.ToInt32(ocorrencias.Split("/")[1]);
                    }
                    else
                    {
                        size = ocorrencias;
                        ocurrs = 0;
                    }
                    str_parameters += "\"" + Convert.ToString(str_params.Split("|")[0]) + "\":{" +
                                      "\"DATATYPE\":\"" + Convert.ToString(str_params.Split("|")[1]) + "\"," +
                                      "\"SIZE\":\"" + size + "\"," +
                                      "\"OCORRENCIAS\":"+ ocurrs + ", " +
                                      "\"SENTIDO\":\"In\"" +
                                      "},";
                }


                string str_json = "{" +
                                  "\"progid\":\"\"," +
                                  "\"progname\":\"\"," +
                                  "\"setup\":\"\"," +
                                  "\"parameters\":{" + str_parameters +
                                  "}" +
                                  "}";

                str_json = str_json.Replace(",}}", "}}").Replace(" ","");
                //var json = JsonSerializer.Serialize(str_json);
                Console.WriteLine("str_json = " + str_json);
                Console.WriteLine("Convertido ... \n{0}", str_json);
                Directory.CreateDirectory(@"json");
                File.WriteAllText(@"json\" + System.IO.Path.GetFileNameWithoutExtension(args[0]) + ".json", str_json);


            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ...\n{0}", ex.Message);
            }
        }
    }
}

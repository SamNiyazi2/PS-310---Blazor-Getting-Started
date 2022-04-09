
// 04/05/2022 07:35 am - SSN 

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ps_310_BethantysPieShopHRM.Shared
{
    public class ErrorMessageCustom
    {
        public ErrorMessageCustom()
        {
        }

        public ErrorMessageCustom(int sequenceNo, string propName)
        {
            PropName = propName;
            SequenceNo = sequenceNo;
        }

        public int SequenceNo { get; set; }
        public string PropName { get; set; }
        public List<String> ErrorMesages { get; set; } = new List<string>();
    }

    public class ErrorMessagesList
    {
        public List<ErrorMessageCustom> errorMessagesList { get; set; } = new List<ErrorMessageCustom>();
        public int sequenceNo { get; set; }

        public void AddEntry(string propName, string errorMessage)
        {
            ErrorMessageCustom emc = errorMessagesList.Find(r => r.PropName == propName);
            if (emc == null)
            {
                emc = new ErrorMessageCustom(++sequenceNo , propName);
                errorMessagesList.Add(emc);
            }
            emc.ErrorMesages.Add(errorMessage);
        }

        internal void AddEntry(string propName, object obj )
        {
            AddEntry(propName, $"{obj}  |  {obj.GetType()}");
        }

        public void AddEntry(string propName, Exception ex)
        {
            AddEntry(propName, ex.Message);
            if (ex.InnerException != null)
            {
                Exception iex = ex.InnerException;
                while (iex != null)
                {
                    AddEntry(propName, iex.Message);
                    iex = iex.InnerException;
                }
            }
        }

        public static void streamToText(Stream stream , out string returnString )
        {
            StreamReader sr = new StreamReader(stream);
            returnString = sr.ReadToEnd();
        }

        public static JsonSerializerOptions getJsonOptions()
        {
            return new JsonSerializerOptions
            {
                //PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                // Not fully implemented.  Not needed.
                //Converters =
                //    {
                //        new ErrorMessagesListConverter()
                //    }
            };
        }

    }


    // https://docs.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-converters-how-to?pivots=dotnet-6-0
    // https://docs.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-migrate-from-newtonsoft-how-to?pivots=dotnet-6-0#specify-constructor-to-use-when-deserializing

    public class ErrorMessagesListConverter : JsonConverter<ErrorMessagesList>
    {
        public override ErrorMessagesList Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            ErrorMessagesList list = new ErrorMessagesList();
            list.AddEntry("step1", "step1");

            list.AddEntry("step2", "step2");

            bool done = false;

            while (!done )
            {
                done =! reader.Read();
                if (done)
                {
                    return list;
                }
                list.AddEntry("step3a", reader.TokenType);

                JsonTokenType temp1 = reader.TokenType;
                list.AddEntry("step3b", temp1);

                //if (reader.TokenType == JsonTokenType.EndObject)
                //{
                //    return list;
                //}
                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    list.AddEntry("step4", reader.GetString());
                    done =! reader.Read();
                    if (!done)
                    {
                        list.AddEntry("step3ccc", reader.TokenType);

                        //string tempString = reader.GetString();
                        //if (string.IsNullOrWhiteSpace (tempString))
                        //{
                        //    done = true;
                        //}
                        //else
                        //{
                        //    list.AddEntry("step5", reader.GetString());
                        //}
                    }
                }
                continue;

                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    string propertyName = reader.GetString();

                 

                    switch (propertyName)
                    {
                        case "CreditLimit":
                            decimal creditLimit = reader.GetDecimal();
                    //        ((Customer)person).CreditLimit = creditLimit;
                            break;
                        case "OfficeNumber":
                            string officeNumber = reader.GetString();
                      //      ((Employee)person).OfficeNumber = officeNumber;
                            break;
                        case "Name":
                            string name = reader.GetString();
                        //    person.Name = name;
                            break;
                    }
                }
            }
  

            //return reader.TokenType switch
            //{
            //    JsonTokenType.True => true,
            //    JsonTokenType.False => false,
            //    JsonTokenType.Number when reader.TryGetInt64(out long l) => l,
            //    JsonTokenType.Number => reader.GetDouble(),
            //    JsonTokenType.String when reader.TryGetDateTime(out DateTime datetime) => datetime,
            //    JsonTokenType.String => reader.GetString()!,
            //    _ => JsonDocument.ParseValue(ref reader).RootElement.Clone()
            //};

            ///////////////////////////////////////////////     string temp = ErrorMessagesList.streamToText(  reader , out string stringValue);

            return list;
        }

        public override void Write(Utf8JsonWriter writer, ErrorMessagesList list, JsonSerializerOptions options)
        {
           //////////////////////////////// JsonSerializer.Serialize(writer, list);
        }
    }
}

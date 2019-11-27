using System;
using System.Collections.Generic;
using System.Text;
using Bogus;
using System.Text.Json;

namespace EHSender
{
    class DataGenerator
    {

        public string generateDateJson()
        {
            DeviceReading data = generateData();
            return JsonSerializer.Serialize<DeviceReading>(data);
        }

        public DeviceReading generateData()
        {
            string[] deviceIDs = { "LHJ1879", "FRZ7757", "KPB4226", "NOX3665", "HQF2700", "AYE9130", "ZCS2987", "NIJ2249", "SMV1039", "ATL9563", "QWK0965", "MFB7787", "IWB5512", "MJV0919", "IGH4138", "LYE3842", "GTK2517", "XJM9511", "NPZ9461", "YUI6488", "XWF7701", "PKU3924", "XUM5269", "JFH3813", "ZCR1389", "MGD8099", "EEQ5370", "ZVB1195", "RXL9986", "IDF6343", "OIS1701", "ARU4429", "GPZ1948", "ISY1128", "BLW0264", "MIZ4936", "GQM4560", "NGN4052", "QUA5739", "FYJ1657", "HZR0010", "BLR5675", "EJQ2028", "NZX1060", "SDI9122", "HWJ9315", "IIB0540", "OTY5297", "AUW9503", "IIP5428", "NHG7121", "QWD5191", "JKK8342", "BNA8020", "ZLO0110", "MSV1653", "XTM3019", "YFR1864", "TFX0016", "KOL0809", "SOR6881", "UNM9000", "GXX3566", "MYC6475", "EBH2102", "QZN9729", "SKC9161", "RNS2422", "QPY4166", "MER9076", "XKJ7377", "OZT3035", "NPA6800", "KOM6632", "FLA0857", "CJO1440", "PFE0219", "PRI1765", "WXD6353", "OSF2874", "GLM3698", "IIN3226", "URX1922", "LLM1870", "MYW4353", "FWV1675", "OPH0277", "AIC8517", "AAE7278", "JRY9725", "VBZ2568", "WED5924", "PTO8352", "WRK4971", "XJC5422", "GDQ9581", "VAD2537", "YKC4639", "JPV5969", "VBB5023", "QYV8600", "RGF4349", "YAD8828", "NUL5220", "ZCS5143", "MGO9297", "JQE6298", "AXQ1040", "EKH4897", "DNX2964", "TOX6211", "DHY9219", "EBK0923", "VHL4902", "DGJ2332", "ITM7290", "DIO6879", "LME0419", "HLF2190", "APR9868", "IYH6236", "YXU1226", "UEG8230", "HHN1091", "TWS0111", "OXI1823", "NKD9632", "KLA6370", "QTP2351", "ACD1152", "HXS3067", "XKE4693", "LRK7309", "DTX5715", "HZV4791", "NHG2127", "JLV6685", "LSI8431", "EVU3526", "KIY9919", "JDN2817", "IBX0398", "HMP2603", "MMX5919", "CXS7660", "PVQ6138", "TXG1371", "WIR2158", "RPQ8072", "EFK6276", "RZP4657", "ORK6382", "OAL4018", "FAV1565", "RWN0941", "JCT5011", "YFE6906", "FYE2674", "ZUH0819", "VXQ4401", "YRY9072", "UQO6694", "YIU3674", "HMM1011", "HOF5942", "GGH5011", "ETK2148", "PVA7276", "FLT0515", "BJD6516", "BNU6521", "SBX8726", "TES6660", "UNO4960", "JHQ6225", "HGE7555", "KVF8983", "FKZ8562", "AGF5503", "UHD4014", "YOY1154", "RJP0871", "WQV7522", "ZZC9415", "OCI9682", "WAR4183", "XPO7219", "QHT4929", "OVF6381", "HGB2999", "PEI3491", "JOF1068", "UOM7632", "FLS8026", "JNN3290", "DLC5281", "HAM3124", "RPM2574", "SPO3640", "ARV7786" };
            var testDevices = new Faker<DeviceReading>()
                .RuleFor(d => d.deviceID, f => f.PickRandomParam<string>(deviceIDs))
                .RuleFor(d => d.readingDate, f => f.Date.Recent())
                .RuleFor(d => d._partitionKey, (f, d) => d.deviceID + "-" + d.readingDate.Year)
                .RuleFor(d => d.readingLatitude, f => f.Random.Decimal(-90, 90))
                .RuleFor(d => d.readingLogitude, f => f.Random.Decimal(-180, 180))
                .RuleFor(d => d.readingPressure, f => f.Random.Number(200))
                .RuleFor(d => d.level, f => f.Random.Number(50))
                .RuleFor(d => d.deviceStatus, f => f.Random.Replace("?##"));

            var devices = testDevices.Generate();
            return devices;
        }
    }


    class DeviceReading
    {
        public string _partitionKey;
        public string deviceID;
        public DateTime readingDate;
        public decimal readingLatitude;
        public decimal readingLogitude;
        public decimal readingPressure;
        public decimal level;
        public string deviceStatus;
    }

}

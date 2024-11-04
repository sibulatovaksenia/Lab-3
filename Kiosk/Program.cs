using System;
using System.Collections.Generic;
using System.Xml;

namespace Kiosk
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string xmlFile = @"C:\Programming\.vs\Programming\v17\lab2\KioskXml\Kiosk\Kiosk\KioskList.xml";

            // Створення XML-файлу
            using (XmlTextWriter writer = new XmlTextWriter(xmlFile, null))
            {
                writer.WriteStartDocument(); // <?xml version="1.0" ?>
                writer.WriteComment("Created @ " + DateTime.Now.ToString());
                writer.WriteStartElement("kioskList"); // <kioskList>

                // Перший елемент
                writer.WriteStartElement("item");
                writer.WriteAttributeString("type", "газета"); // Записуємо атрибут "type"
                writer.WriteElementString("name", "Newspaper A");
                writer.WriteElementString("quantity", "34");
                writer.WriteElementString("price", "17");
                writer.WriteEndElement();

                // Другий елемент
                writer.WriteStartElement("item");
                writer.WriteAttributeString("type", "журнал"); // Записуємо атрибут "type"
                writer.WriteElementString("name", "Magazine A");
                writer.WriteElementString("quantity", "14");
                writer.WriteElementString("price", "40");
                writer.WriteEndElement();

                // Третій елемент
                writer.WriteStartElement("item");
                writer.WriteAttributeString("type", "газета"); // Записуємо атрибут "type"
                writer.WriteElementString("name", "Newspaper B");
                writer.WriteElementString("quantity", "33");
                writer.WriteElementString("price", "15");
                writer.WriteEndElement();

                // Четвертий елемент
                writer.WriteStartElement("item");
                writer.WriteAttributeString("type", "журнал"); // Записуємо атрибут "type"
                writer.WriteElementString("name", "Magazine B");
                writer.WriteElementString("quantity", "10");
                writer.WriteElementString("price", "20");
                writer.WriteEndElement();

                writer.WriteEndElement(); // </kioskList>
                writer.WriteEndDocument(); // маркер кінця документу
            }

            Console.WriteLine("Файл Kiosk.xml створено.");


            var kioskList = new List<dynamic>();
            using (XmlTextReader reader = new XmlTextReader(xmlFile))
            {
                while (reader.Read()) // поки не кінець xml-документа
                {
                    if (reader.NodeType == XmlNodeType.Element && reader.Name == "item")
                    {
                        var type = reader.GetAttribute("type");
                        reader.ReadStartElement("item");
                        var name = reader.ReadElementString("name");
                        var strQuantity = reader.ReadElementString("quantity");
                        var strPrice = reader.ReadElementString("price");

                        // додаємо зчитані дані до списку
                        kioskList.Add(new
                        {
                            name,
                            type,
                            quantity = Convert.ToInt32(strQuantity),
                            price = Convert.ToDecimal(strPrice)
                        });
                    }
                }
            }

            // Вивід зчитаних даних на консоль
            Console.WriteLine("Виведення даних з Kiosk.xml:");
            foreach (var item in kioskList)
            {
                Console.WriteLine($"Назва: {item.name}, Тип: {item.type}, Кількість: {item.quantity}, Ціна: {item.price}");
            }


            // Завдання: знайти загальну вартість усіх газет
            decimal totalCost = 0;
            int journalCount = 0;
            decimal lowerBound = 15; // Значення X
            decimal upperBound = 35; // Значення Y

            foreach (var item in kioskList)
            {
                if (item.type == "газета")
                {
                    totalCost += item.quantity * item.price;
                }

                if (item.type == "журнал" && item.price >= lowerBound && item.price <= upperBound)
                {
                    journalCount++;
                }
            }

            Console.WriteLine($"Загальна вартість усіх газет: {totalCost}");
            Console.WriteLine($"Кількість журналів із ціною від {lowerBound} грн. до {upperBound} грн.: {journalCount}");

            Console.ReadKey();
        }
    }
}

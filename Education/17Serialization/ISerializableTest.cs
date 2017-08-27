using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Education._17Serialization
{
    public class ISerializableTest
    {
        public void DoSmth()
        {
            //SimpleTestBinary();
            SimpleTestXml();
        }

        //private void SimpleTestBinary()
        //{
        //    Person p1 = new Person() { Name = "Stepan Bandera", DateOfBirth = new DateTime(1909, 01, 01) };
        //    Person p2 = new Person() { Name = "Roman Shuhevych", DateOfBirth = new DateTime(1907, 07, 30) };

        //    Team team = new Team() { Name = "Banderivtsi", Players = new List<Person>() { p1, p2 } };

        //    var formatter = new BinaryFormatter();

        //    using (FileStream fs = File.Create("somefile.bin"))
        //    {
        //        formatter.Serialize(fs, team);
        //    }

        //    using (FileStream fs = File.OpenRead("somefile.bin"))
        //    {
        //        var teamBand = (Team)formatter.Deserialize(fs);
        //        Console.WriteLine(teamBand.Name + " " + teamBand.Players[0].Name + " " + teamBand.Players[1].Name);
        //    }
        //}

        private void SimpleTestXml()
        {
            Person p = new Person() { Name = "Stepan Bandera", Age = 19 };
            var xmlSerializer = new XmlSerializer(typeof(Person));

            using (var stream = File.Create("something.xml"))
            {
                xmlSerializer.Serialize(stream, p);
            }

            Person p2;
            using (var stream = File.OpenRead("something.xml"))
            {
                p2 = (Person)xmlSerializer.Deserialize(stream);
            }

            Console.WriteLine(p2.Name);
        }
    }

    [XmlRoot ("Candidate", Namespace= "http://somethingSpecial")]
    public class Person
    {
        public string Name;

        [XmlAttribute("The name")]
        public int Age;
    }

    //[Serializable]
    //public class Person
    //{
    //    public string Name;

    //    public DateTime DateOfBirth;

    //    //public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
    //    //{
    //    //    info.AddValue("Name", Name);
    //    //    info.AddValue("DateOfBirth", DateOfBirth);
    //    //}

    //    //public Person() { }

    //    //public Person(SerializationInfo info, StreamingContext context)
    //    //{
    //    //    Name = info.GetString("Name");
    //    //    DateOfBirth = info.GetDateTime("DateOfBirth");
    //    //}
    //}

    //[Serializable]
    //public class Team : ISerializable
    //{
    //    public string Name;

    //    public List<Person> Players;

    //    public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
    //    {
    //        info.AddValue("Name", Name);
    //        info.AddValue("PlayerData", Players.ToArray());
    //    }

    //    public Team() { }

    //    public Team(SerializationInfo info, StreamingContext context)
    //    {
    //        Name = info.GetString("Name");
    //        var playerData = info.GetValue("PlayerData", typeof(object));
    //        Person[] persons = (Person[])info.GetValue("PlayerData", typeof(Person[]));
    //        Players = new List<Person>(persons);
    //    }
    //}
}

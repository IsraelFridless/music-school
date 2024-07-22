using MusicSchool.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static MusicSchool.Configuration.MusicSchoolConfiguration;

namespace MusicSchool.Service
{
    internal static class MusicSchoolService
    {
        public static void test()
        {
            MessageBox.Show("Bla Bla!!!");
        }

        public static void CreateXmlIfNotExists()
        {
            if (!File.Exists(musicSchoolPath))
            {
                // create new document (xml)
                XDocument document = new();
                // create an element
                XElement musicSchool = new("music-school");
                // add element to the document
                document.Add(musicSchool);
                // save the path
                document.Save(musicSchoolPath);
            }
        }

        public static void InsertClassroom(string classRoomName)
        {
            XDocument document = XDocument.Load(musicSchoolPath);
            XElement? musicSchool = document.Descendants("music-school").FirstOrDefault();
            if (musicSchool == null) {return; }

            XElement classRoom = new("class-room", new XAttribute ("name", classRoomName));
            musicSchool.Add(classRoom);

            document.Save(musicSchoolPath);
        }

        public static void AddTeacher(string classRoomName, string teacherName)
        {
            // load document
            XDocument document = XDocument.Load (musicSchoolPath);

            // find the specific class-room by attribute name
            XElement? classRoom = document.Descendants("class-room").
                FirstOrDefault(room => room.Attribute("name")?.Value == classRoomName);

            // create new XElement teacher with attribute name = teacherName
            XElement teacher = new("teacher", new XAttribute("name", teacherName));

            classRoom.Add(teacher);

            document.Save(musicSchoolPath);
        }

        public static void AddStudent(string classRoomName, string studentName, string instrumentName)
        {
            XDocument document = XDocument.Load(musicSchoolPath);
            XElement? classRoom = document.Descendants(
                "class-room").FirstOrDefault(room => room.Attribute("name")?.Value == classRoomName);

                XElement? student = new(
                "student", new XAttribute("name", studentName), 
                new XElement("instrument", instrumentName)
                );

            classRoom.Add(student);
            document.Save(musicSchoolPath);
        }

        public static XElement? ConvertModelToXelement(StudentModel studentModel)
        {
            XElement? student = new(
                "student", new XAttribute("name", studentModel.Name),
                new XElement("instrument", studentModel.Instrument.Name)
                );
            return student;
        }

        public static void AddManyStudents(string classRoomName, params StudentModel[] students)
        {
            XDocument document = XDocument.Load(musicSchoolPath);
            XElement? classRoom = document.Descendants(
                "class-room").FirstOrDefault(room => room.Attribute("name")?.Value == classRoomName);

            List<XElement?> Xstudents = students.Select(student => ConvertModelToXelement(student)).ToList();
            classRoom.Add(Xstudents);
            document.Save(musicSchoolPath);
        }

        public static void UpdateInstrument(string studentName, InstrumentModel instrument)
        {
            XDocument document = XDocument.Load (musicSchoolPath);
            XElement? student = document.Descendants(
                "student").FirstOrDefault(student => student.Attribute("name")?.Value == studentName);

            student.SetElementValue("instrument", instrument.Name);
            document.Save(musicSchoolPath);
        }

        public static void UpdateTeacherName(string oldName, string newName)
        {
            XDocument document = XDocument.Load(musicSchoolPath);
            XElement teacher = document.Descendants(
                "teacher").FirstOrDefault(teacher => teacher.Attribute("name")?.Value == oldName);
            teacher.SetAttributeValue("name", newName);
            document.Save(musicSchoolPath);
        }

        public static void ReplaceStudent(StudentModel studentModel, string otherStudent)
        {
            XDocument document = XDocument.Load(musicSchoolPath);
            XElement student = document.Descendants(
                "student").FirstOrDefault(student => student.Attribute("name")?.Value == otherStudent);
            XElement studentElement = ConvertModelToXelement(studentModel);

        }

    }
}

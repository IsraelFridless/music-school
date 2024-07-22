using MusicSchool.Model;
using MusicSchool.Service;
using static MusicSchool.Service.MusicSchoolService;

namespace MusicSchool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            CreateXmlIfNotExists();
            UpdateTeacherName("Enosh", "Enosh the king");
        }
    }
}

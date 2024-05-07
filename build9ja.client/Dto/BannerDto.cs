using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace build9ja.client.Dto
{
    public class BannerDto
    {
        public long Id {get; set; }
         public string ImageOne { get; set; }
        public string TitleOne { get; set; }
        public string SubTitleOne { get; set; }
        public string LinkOne { get; set; }

        public string ImageTwo { get; set; }
        public string TitleTwo { get; set; }
        public string SubTitleTwo { get; set; }
        public string LinkTwo { get; set; }

        public string ImageThree { get; set; }
        public string TitleThree { get; set; }
        public string SubTitleThree { get; set; }
        public string LinkThree { get; set; }

        public string ImageFour { get; set; }
        public string TitleFour { get; set; }
        public string SubTitleFour { get; set; }
        public string LinkFour { get; set; }

        public string SubPageImage { get; set; }
    }
}
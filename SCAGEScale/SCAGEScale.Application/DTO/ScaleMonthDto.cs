using SCAGEScale.Application.VO;

namespace SCAGEScale.Application.DTO
{
    public class ScaleMonthDto
    {
        public ReferencyUser CameraOne { get; set; }
        public ReferencyUser CameraTwo { get; set; }
        public ReferencyUser CutDesk { get; set; }

        public ScaleMonthDto(ReferencyUser userOne, ReferencyUser userTwo, ReferencyUser userThree)
        {
            this.CameraOne = userOne;
            this.CameraTwo = userTwo;
            this.CutDesk = userThree;
        }
    }
}

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using VR.Data.Model;
using VR.Dto.User;

namespace VR.Dto
{
    public class ReportDto
    {
        public ReportDto()
        {
            TodayDate = "";
            TotalLetter = "";
            LegalRulingDescription = "";
            FileNumber = "";
            IsGratherThanZero = true;
            IsZero = true;
            IsLessThanZero = true;
            LineIsLessThanZero = true;
            LineIsGratherThanZero = true;
        }

        public string TodayDate { set; get; }
        public string TotalLetter { set; get; }
        public string LegalRulingDescription { set; get; }
        public string FileNumber { set; get; }
        public Decimal Advance { set; get; }
        public Decimal SupportingPresent { set; get; }
        public Decimal Total { set; get; }
        public bool IsGratherThanZero { set; get; }
        public bool IsLessThanZero { set; get; }
        public bool LineIsLessThanZero { set; get; }
        public bool LineIsGratherThanZero { set; get; }
        public bool IsZero { set; get; }

        public void TotalBool()
        {
            //debo reintegrar
            if (Total > 0)
            {
                LineIsLessThanZero = false;
                IsLessThanZero = false;
            }

            //me deben reembolsar
            if (Total < 0)
            {
                IsGratherThanZero = false;
                LineIsGratherThanZero = false;
            }
            if (Total == 0)
            {
                IsZero = false;
                LineIsLessThanZero = false;
                LineIsGratherThanZero = false;
            }
        }
    }

    public class ReportByDestiniesAndDatesDto
    {
        public Guid? CityId { set; get; }
        public Guid? CountryId { set; get; }
        public int EndDay { set; get; }
        public int EndMonth { set; get; }
        public int EndYear { set; get; }
        public DateDto EndDate { set; get; }
        public Guid? ProvinceId { set; get; }
        public DateDto StartDate { set; get; }
        public int StartDateDay { set; get; }
        public int StartDateMonth { set; get; }
        public int StartDateYear { set; get; }
    }

    public class UserAgentFilterDto : FilterBaseDto
    {
        public string FirstName { set; get; }
        public string LastName { set; get; }
    }

    public class VerificationCommissionDto
    {

        public VerificationCommissionDto()
        {
            AttachTelegamId = new Guid("A4F9F9DD-25F9-4B00-BF9B-5AF239537438");
            CertificationOfAuthorityId = new Guid("3F42469F-E12E-4468-8537-D43391C93604");
            CertificationOfAuthorityThatOrderCommissionId = new Guid("00E77CC4-DC5C-4CB8-B97B-DF141FEFA2B0");
            ReservedCommissionId = new Guid("179d6557-1242-493c-9b4e-bdc28c8903ed");
            UtilizacionOfTaxiId = new Guid("8fb36ea0-0b89-4f57-a2e0-6359e240a4e1");
            ExpenditureTicketId = new Guid("831c2ff8-4495-48b4-bd25-fb7eae75ae1d");
            CommissionOfDirectorsId = new Guid("e755d84e-ab94-425e-b8e0-cbbc19c8d74b");
            SolicitationMonthlyId = new Guid("b74346c1-c7aa-4734-b927-9a19e90364e6");
            InitiationDurationAndExpirationOfCommissionId = new Guid("3168f61f-f69e-464a-b7cd-aa115d4fb1da");
            ComplyWithArticle1Id = new Guid("a3f6bb69-2b60-44a5-8998-5528092f9c2f");
            Percent100Id = new Guid("A67F58FB-441D-4D93-A169-0DC297E1728A");
            Percent75Id = new Guid("E0C796AC-7C47-486D-9E4D-40350D211E39");
            Percent50Id = new Guid("ED50B49E-5979-448C-8417-14357E36B6F8");
            Percent25Id = new Guid("C9102E40-241D-4D4B-A5CB-B2378407956E");
            TopicCId = new Guid("04A287F2-715F-493F-AE42-ED0CABED0CBB");
        }

        private readonly string Uncheck = "iVBORw0KGgoAAAANSUhEUgAAADIAAAA8CAYAAAAkNenBAAAACXBIWXMAAA7DAAAOwwHHb6hkAAADC0lEQVR4nN2YTUgUYRjH/7Pz7K7umrkJsSnbBxRiJBHlRpCRFBGVeCgiEE9R0Llbxw7d6xR0Cu3QVTICC6ElDN3QJKgNQtSgTFODcnWd2emZJPuwg/v6rM/SDwZmDvPs7/++M7Pv85LH4D+AtAWkIG0BKUhbQArSFpCCtAWkIG0BKUhbQArSFpCCtAWkIG0BKUhbQArSFpCCtAWkIG0BKUhbQArSFpCCtAWkIG0BKUhbQArSFiiNIB7vW1iWkIpmEKs0QsA8iL+DtBTCmRjFSE8nOtNjyExlUR60YV4TWJirRHzXfjRfuoDGRBjxQFGD/JyJLMaHnqC74w5uPx7Fp7xZtT8JoWLreURbz2JnbdGDMG4ebvoWeh/cx43Ue0yKhABTgWikDCHbQqCAJ9c4iOu8w8uHj/C0axCTWdMq/8IGUQD+RBTyBhoG+cxBhpHq+YDXY2YVpCGz23LweLi+VDZhz+HjOHc5iRhfh5w8Vv+E8aiTy+/4NDJd99DXO4jUNH88vHUNwrpWCPnqfahrbMDF9iZsMivEzGJoqg9fXwzi2Qxfrm+QJSxvEU4ui298HvOvjarMIZtzsLjGjwWZ3WYt/7N7rgt+QODwESy4jm/v8AeQ65iJYI1BSg/SFpCCtAWkIG0BKUhbQArSFpCCtAWkIG0BKUhbQArSFpCCtAWkIG2BleSQW5jHvFvYkt4wiLe8i2LZNvd0poX8PpBgc0/rL0O9HyvgGCqr49hSbiFqr9OMeFYQFCpHlM/NN4YiiISDsPNLK2gkGtFw5CRaEkEkit+z80h6OQQmnuPV237crBlGFc9OuOAO0eFOYAZvugcwMMuRag6hufU02lr2IrHB/mvjqShBQtxUARvn+pFJZ9CRvmtW5jfKquuw+9hVXGk/ilPJX/3maifFMEgFKHwAJ65dx/aRjxi3AqseuZX471kAkapabN5xEMl6s6bZLIgX5sdiG+rP8GFUQH5LmYx+qchbvpZBfbMgJQhpC0hB2gJSkLaAFKQtIAVpC0hB2gJSkLaAFKQtIMV3dyqw1I+2Le4AAAAASUVORK5CYII=";
        private readonly string Check = "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";

        public string Percent100Text { set; get; }
        public Guid Percent100Id { set; get; }
        public string Percent100Image { set; get; }
        public bool Percent100Checked { set; get; }

        public string Percent75Text { set; get; }
        public Guid Percent75Id { set; get; }
        public string Percent75Image { set; get; }
        public bool Percent75Checked { set; get; }

        public string Percent50Text { set; get; }
        public Guid Percent50Id { set; get; }
        public string Percent50Image { set; get; }
        public bool Percent50Checked { set; get; }

        public string Percent25Text { set; get; }
        public Guid Percent25Id { set; get; }
        public string Percent25Image { set; get; }
        public bool Percent25Checked { set; get; }

        public string TopicCText { set; get; }
        public Guid TopicCId { set; get; }
        public string TopicCImage { set; get; }
        public bool TopicCChecked { set; get; }
        //
        public string AttachTelegamText { set; get; }
        public Guid AttachTelegamId { set; get; }
        public string AttachTelegamImage { set; get; }
        public bool AttachTelegamChecked { set; get; }
        
        public string CertificationOfAuthorityText { set; get; }
        public Guid CertificationOfAuthorityId { set; get; }
        public string CertificationOfAuthorityImage { set; get; }
        public bool CertificationOfAuthorityChecked { set; get; }

        public string CertificationOfAuthorityThatOrderCommissionText { set; get; }
        public Guid CertificationOfAuthorityThatOrderCommissionId { set; get; }
        public string CertificationOfAuthorityThatOrderCommissionImage { set; get; }
        public bool CertificationOfAuthorityThatOrderCommissionChecked { set; get; }

        public string ReservedCommissionText { set; get; }
        public Guid ReservedCommissionId { set; get; }
        public string ReservedImage { set; get; }
        public bool ReservedChecked { set; get; }

        public string UtilizacionOfTaxiText { set; get; }
        public Guid UtilizacionOfTaxiId { set; get; }
        public string UtilizacionOfTaxiImage { set; get; }
        public bool UtilizacionOfTaxiChecked { set; get; }

        public string ExpenditureTicketText { set; get; }
        public Guid ExpenditureTicketId { set; get; }
        public string ExpenditureTicketImage { set; get; }
        public bool ExpenditureTicketChecked { set; get; }

        public string CommissionOfDirectorsText { set; get; }
        public Guid CommissionOfDirectorsId { set; get; }
        public string CommissionOfDirectorsImage { set; get; }
        public bool CommissionOfDirectorsChecked { set; get; }

        public string SolicitationMonthlyText { set; get; }
        public Guid SolicitationMonthlyId { set; get; }
        public string SolicitationMonthlyImage { set; get; }
        public bool SolicitationMonthlyChecked { set; get; }

        public string InitiationDurationAndExpirationOfCommissionText { set; get; }
        public Guid InitiationDurationAndExpirationOfCommissionId { set; get; }
        public string InitiationDurationAndExpirationOfCommissionImage { set; get; }
        public bool InitiationDurationAndExpirationOfCommissionChecked { set; get; }

        public string ComplyWithArticle1Text { set; get; }
        public Guid ComplyWithArticle1Id { set; get; }
        public string ComplyWithArticle1Image { set; get; }
        public bool ComplyWithArticle1Checked { set; get; }

        public string CreateImage(string nameImage)
        {
            FileInfo fileInfo = new FileInfo(Path.Combine(Directory.GetCurrentDirectory(), "StaticFiles", nameImage));

            var outputStream = new MemoryStream();

            using (var image = Image.Load(fileInfo.FullName))
            {
                image.Mutate(x => x
                    .Resize(50, 60));

                image.SaveAsPng(outputStream);

                outputStream.Seek(0, SeekOrigin.Begin);

                byte[] bytes = outputStream.ToArray();

                return Convert.ToBase64String(bytes);
            }
        }

        public void SetValues(List<ApproveOfAuthorityThatOrderCommissionsDto> list)
        {

            var attachObj = list.FirstOrDefault(x => x.Id == AttachTelegamId);
            AttachTelegamText = attachObj.Description;
            AttachTelegamChecked = attachObj.Checked;
            if (attachObj.Checked)
            {
                AttachTelegamImage = Check;
            }
            else
            {
                AttachTelegamImage = Uncheck;
            }
            

            var CertificationOfAuthorityObj = list.FirstOrDefault(x =>x.Id == CertificationOfAuthorityId);
            CertificationOfAuthorityText = CertificationOfAuthorityObj.Description;
            CertificationOfAuthorityChecked = CertificationOfAuthorityObj.Checked;
            if (CertificationOfAuthorityObj.Checked)
            {

                CertificationOfAuthorityImage = Check;
            }
            else
            {
                CertificationOfAuthorityImage = Uncheck;
            }

            var CertificationOfAuthorityThatOrderCommissionIbj = list.FirstOrDefault(x =>x.Id == CertificationOfAuthorityThatOrderCommissionId);
            CertificationOfAuthorityThatOrderCommissionText = CertificationOfAuthorityThatOrderCommissionIbj.Description;
            CertificationOfAuthorityThatOrderCommissionChecked = CertificationOfAuthorityThatOrderCommissionIbj.Checked;
            if (CertificationOfAuthorityThatOrderCommissionIbj.Checked)
            {
                CertificationOfAuthorityThatOrderCommissionImage = Check;
            }
            else
            {
                CertificationOfAuthorityThatOrderCommissionImage = Uncheck;
            }

            var ReservedCommission = list.FirstOrDefault(x => x.Id == ReservedCommissionId);
            ReservedCommissionText = ReservedCommission.Description;
            ReservedChecked = ReservedCommission.Checked;
            if (ReservedCommission.Checked)
            {
                ReservedImage = Check;
            }
            else
            {
                ReservedImage = Uncheck;
            }

            var UtilizacionOfTaxi = list.FirstOrDefault(x => x.Id == UtilizacionOfTaxiId);
            UtilizacionOfTaxiText = UtilizacionOfTaxi.Description;
            UtilizacionOfTaxiChecked = UtilizacionOfTaxi.Checked;
            if (UtilizacionOfTaxi.Checked)
            {
                UtilizacionOfTaxiImage = Check;
            }
            else
            {
                UtilizacionOfTaxiImage = Uncheck;
            }

            var ExpenditureTicket = list.FirstOrDefault(x => x.Id == ExpenditureTicketId);
            ExpenditureTicketText = ExpenditureTicket.Description;
            ExpenditureTicketChecked = ExpenditureTicket.Checked;
            if (ExpenditureTicket.Checked)
            {
                ExpenditureTicketImage = Check;
            }
            else
            {
                ExpenditureTicketImage = Uncheck;
            }

            var CommissionOfDirectors = list.FirstOrDefault(c => c.Id == CommissionOfDirectorsId);
            CommissionOfDirectorsText = CommissionOfDirectors.Description;
            CommissionOfDirectorsChecked = CommissionOfDirectors.Checked;
            if (CommissionOfDirectors.Checked)
            {
                CommissionOfDirectorsImage = Check;
            }
            else
            {
                CommissionOfDirectorsImage = Uncheck;
            }

            var SolicitationMonthly = list.FirstOrDefault(v => v.Id == SolicitationMonthlyId);
            SolicitationMonthlyText = SolicitationMonthly.Description;
            SolicitationMonthlyChecked = SolicitationMonthly.Checked;
            if (SolicitationMonthly.Checked)
            {
                SolicitationMonthlyImage = Check;
            }
            else
            {
                SolicitationMonthlyImage = Uncheck;
            }

            var InitiationDurationAndExpirationOfCommission =
                list.FirstOrDefault(x => x.Id == InitiationDurationAndExpirationOfCommissionId);
            InitiationDurationAndExpirationOfCommissionText = InitiationDurationAndExpirationOfCommission.Description;
            InitiationDurationAndExpirationOfCommissionChecked = InitiationDurationAndExpirationOfCommission.Checked;
            if (InitiationDurationAndExpirationOfCommission.Checked)
            {
                InitiationDurationAndExpirationOfCommissionImage = Check;
            }
            else
            {
                InitiationDurationAndExpirationOfCommissionImage = Uncheck;
            }

            var ComplyWithArticle1 = list.FirstOrDefault(c => c.Id == ComplyWithArticle1Id);
            ComplyWithArticle1Text = ComplyWithArticle1.Description;
            ComplyWithArticle1Checked = ComplyWithArticle1.Checked;
            if (ComplyWithArticle1.Checked)
            {
                ComplyWithArticle1Image = Check;
            }
            else
            {
                ComplyWithArticle1Image = Uncheck;
            }


            var percent100 = list.FirstOrDefault(c => c.Id == Percent100Id);
            Percent100Text = percent100.Description;
            Percent100Checked = percent100.Checked;
            if (percent100.Checked)
            {
                Percent100Image = Check;
            }
            else
            {
                Percent100Image = Uncheck;
            }

            var percent75 = list.FirstOrDefault(c => c.Id == Percent75Id);
            Percent75Text = percent75.Description;
            Percent75Checked = percent75.Checked;
            if (percent75.Checked)
            {
                Percent75Image = Check;
            }
            else
            {
                Percent75Image = Uncheck;
            }

            var percent50 = list.FirstOrDefault(c => c.Id == Percent50Id);
            Percent50Text = percent50.Description;
            Percent50Checked = percent50.Checked;
            if (percent50.Checked)
            {
                Percent50Image = Check;
            }
            else
            {
                Percent50Image = Uncheck;
            }

            var percent25 = list.FirstOrDefault(c => c.Id == Percent25Id);
            Percent25Text = percent25.Description;
            Percent25Checked = percent25.Checked;
            if (percent25.Checked)
            {
                Percent25Image = Check;
            }
            else
            {
                Percent25Image = Uncheck;
            }
            var TopicC = list.FirstOrDefault(c => c.Id == TopicCId);
            TopicCText = TopicC.Description;
            TopicCChecked = TopicC.Checked;
            if (TopicC.Checked)
            {
                TopicCImage = Check;
            }
            else
            {
                TopicCImage = Uncheck;
            }
        }
        }
    }
﻿using NSubstitute;
using SmartTechnologiesM.Activation;
using System;
using Xunit;

namespace SmartTechnologiesM.Testing
{
    public class ActivationTesting
    {
        private readonly string _key = "1f5gF$7gn5ugRj5uHfk&lg548G5m*ff6";
        private readonly string _iv = "fTG%85jGfi@4j*Ei";

        [Fact]
        public void NoLicenseTesting()
        {
            var compressor = Substitute.For<ICompressor>();
            var hardwareProvider = Substitute.For<IHardwareInfoProvider>();
            var activationFile = Substitute.For<IActivationFile>();
            activationFile.Exists().Returns(false);
            var manager = new ActivationManager(_key, _iv, compressor, activationFile, hardwareProvider);
            var actualLicenseInfo = manager.ActualLicenseInfo;
            Assert.Equal(string.Empty, actualLicenseInfo.RequestCode);
        }

        [Fact]
        public void RequestCodeTesting()
        {
            var compressor = Substitute.For<ICompressor>();
            var hardwareProvider = Substitute.For<IHardwareInfoProvider>();
            hardwareProvider.DriveSerial.Returns("A");
            hardwareProvider.MemorySerial.Returns("A");
            hardwareProvider.MotherboardSerial.Returns("A");
            hardwareProvider.ProcessorId.Returns("A");
            var activationFile = Substitute.For<IActivationFile>();
            activationFile.Exists().Returns(false);
            var manager = new ActivationManager(_key, _iv, compressor, activationFile, hardwareProvider);
            var requestCode = manager.GetRequestCode();
            Assert.Equal("0988-90DD-E069-E9AB-AD63-F19A-0D9E-1F32", requestCode);
        }

        [Fact]
        public void GetActivationKeyTesting()
        {
            var compressor = Substitute.For<ICompressor>();
            var hardwareProvider = Substitute.For<IHardwareInfoProvider>();
            var activationFile = Substitute.For<IActivationFile>();
            activationFile.Exists().Returns(false);
            var manager = new ActivationManager(_key, _iv, compressor, activationFile, hardwareProvider);
            var activationKey = manager.GetActivationKey(new LicenseInfo
            {
                RequestCode = "0123-4567-89AB-CDEF-0000-0000-0000-0000",
                ExpirationDate = new DateTime(2025, 1, 1)
            });
            Assert.Equal("1AA7-570E-6DA5-E7CB-8727-3337-E457-3244", activationKey);
        }

        [Fact]
        public void TryActivateTesting()
        {
            var compressor = Substitute.For<ICompressor>();
            var hardwareProvider = Substitute.For<IHardwareInfoProvider>();
            hardwareProvider.DriveSerial.Returns("A123");
            hardwareProvider.MemorySerial.Returns("11dd4");
            hardwareProvider.MotherboardSerial.Returns("asfd345");
            hardwareProvider.ProcessorId.Returns("236");
            var activationFile = Substitute.For<IActivationFile>();
            activationFile.Exists().Returns(false);
            var manager = new ActivationManager(_key, _iv, compressor, activationFile, hardwareProvider);
            var requestCode = manager.GetRequestCode();
            var expirationDate = new DateTime(2025, 4, 3);
            var activationKey = manager.GetActivationKey(new LicenseInfo
            {
                RequestCode = requestCode,
                ExpirationDate = expirationDate
            });
            Assert.True(manager.TryActivate(activationKey, out LicenseInfo licenseInfo));
            Assert.Equal(expirationDate.Year, licenseInfo.ExpirationDate.Year);
            Assert.Equal(expirationDate.Month, licenseInfo.ExpirationDate.Month);
            Assert.Equal(expirationDate.Day, licenseInfo.ExpirationDate.Day);
        }
    }
}

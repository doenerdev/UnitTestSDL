using Xunit;

namespace Tests.ResistanceToRefactoring.PrivateState;

//Productive Code
public class BusinessPartner()
{
    private VerificationStatus _status = VerificationStatus.Unverified;

    public bool IsEligibleForLeaseContracts() => _status switch
    {
        VerificationStatus.Verified => true,
        _ => false
    };

    public void Verify()
    {
        _status = VerificationStatus.Verified;
    }
}


//Test Code
public class BusinessPartnerTests
{
    [Fact]
    public void IsEligibleForLeaseContracts_NewBusinessPartner_ReturnsFalse()
    {
        //Arrange
        var partner = new BusinessPartner();

        //Act
        var actual = partner.IsEligibleForLeaseContracts();

        //Assert
        Assert.False(actual);
    }

    [Fact]
    public void IsEligibleForLeaseContracts_VerifiedBusinessPartner_ReturnsFalse()
    {
        //Arrange
        var partner = new BusinessPartner();
        partner.Verify();

        //Act
        var actual = partner.IsEligibleForLeaseContracts();

        //Assert
        Assert.True(actual);
    }
}




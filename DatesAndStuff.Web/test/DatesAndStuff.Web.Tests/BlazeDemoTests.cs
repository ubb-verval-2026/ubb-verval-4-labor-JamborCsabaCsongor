using System;
using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace DatesAndStuff.Web.Tests;

[TestFixture]
public class BlazeDemoTests
{
    private IWebDriver driver;

    [SetUp]
    public void Setup()
    {
        driver = new ChromeDriver();
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
    }

    [TearDown]
    public void Teardown()
    {
        driver.Quit();
        driver.Dispose();
    }

    [Test]
    public void FlightSearch_MexicoCityToDublin_ShouldHaveAtLeastThreeFlights()
    {
        driver.Navigate().GoToUrl("https://blazedemo.com/");

        // indulasi hely
        var departureDropdown = new SelectElement(driver.FindElement(By.Name("fromPort")));
        departureDropdown.SelectByValue("Mexico City");

        // erkezesi hely
        var destinationDropdown = new SelectElement(driver.FindElement(By.Name("toPort")));
        destinationDropdown.SelectByValue("Dublin");

        // kereses gomb
        var submitButton = driver.FindElement(By.CssSelector("input[type='submit']"));
        submitButton.Click();

        // eredmenyek a tablazatban
        var flightRows = driver.FindElements(By.CssSelector("table tbody tr"));

        // 3 jarat??
        flightRows.Count.Should().BeGreaterThanOrEqualTo(3, "Legalabb 3 jaratnak kell lennie, mert ha nem akkor nagy baj van...");
    }
}

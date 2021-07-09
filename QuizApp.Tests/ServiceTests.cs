using QuizApp.Entities;
using QuizApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace QuizApp.Tests
{
    public class ServiceTests
    {
        private readonly TreeService _sut;

        public ServiceTests()
        {
            _sut = new TreeService(new(new()));
        }

        [Fact]
        public void FlattenShouldEqualAllSubfoldersWithRoot()
        {
            // Arrange
            var folder = new Folder
            {
                Subfolders = new List<Folder>
                {
                    new Folder
                    {
                        Subfolders = new List<Folder>
                        {
                            new Folder {}
                        }
                    }
                }
            };

            // Act
            var result = _sut.FlattenSubfolders(folder).Count();

            // Assert
            Assert.Equal(3, result);
        }
    }
}

using NUnit.Core;
using UnityEngine;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace Player
{
    [TestFixture]
    [Category("Player")]
    internal class PlayerMovementTests
    {
        [Test]
        public void ShouldFallIfNotOnGround()
        {
            var movement = new PlayerMovement {IsOnGround = false};

            movement.UpdateCurrentVelocity();

            movement.CurrentVelocity.y.Should().BeLessThan(0.0f);
        }

        [Test]
        public void ShouldNotFallIfOnGround()
        {
            var movement = new PlayerMovement { IsOnGround = true };

            movement.UpdateCurrentVelocity();

            movement.CurrentVelocity.y.Should().Be(0.0f);
        }

        [Test]
        public void ShouldAccelerateWhilstFalling()
        {
            var movement = new PlayerMovement { IsOnGround = false };

            movement.UpdateCurrentVelocity();
            var firstResult = movement.CurrentVelocity;
            movement.UpdateCurrentVelocity();

            movement.CurrentVelocity.y.Should().BeLessThan(firstResult.y);
        }

        [TestCase(1.0f, 10.0f, 10.0f, TestName = "Full Speed Left")]
        [TestCase(0.5f, 10.0f, 5.0f, TestName = "Half Speed Left")]
        [TestCase(-0.5f, 10.0f, -5.0f, TestName = "Half Speed Right")]
        [TestCase(-1.0f, 10.0f, -10.0f, TestName = "Full Speed Right")]
        public void ShouldRespondToHorizontalInput(float input, float maxSpeed, float expectedResult)
        {
            var movement = new PlayerMovement
            {
                HorizontalInput = input,
                MaxHorizontalSpeed = maxSpeed
            };

            movement.UpdateCurrentVelocity();

            movement.CurrentVelocity.x.Should().BeApproximately(expectedResult, 0.1f);
        }
    }
}
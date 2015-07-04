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
        private const float SomeDeltaTime = 0.25f;

        [Test]
        public void ShouldFallIfNotOnGround()
        {
            var movement = new PlayerMovement {IsOnGround = false};

            movement.UpdateCurrentVelocity(SomeDeltaTime);

            movement.CurrentVelocity.y.Should().BeLessThan(0.0f);
        }

        [Test]
        public void ShouldNotFallIfOnGround()
        {
            var movement = new PlayerMovement { IsOnGround = true };

            movement.UpdateCurrentVelocity(SomeDeltaTime);

            movement.CurrentVelocity.y.Should().Be(0.0f);
        }

        [Test]
        public void ShouldAccelerateWhilstFalling()
        {
            var movement = new PlayerMovement { IsOnGround = false };

            movement.UpdateCurrentVelocity(SomeDeltaTime);
            var firstResult = movement.CurrentVelocity;
            movement.UpdateCurrentVelocity(SomeDeltaTime);

            var accelerationDueToGravity = movement.CurrentVelocity.y - firstResult.y;
            accelerationDueToGravity.Should().BeApproximately(PlayerMovement.Gravity * SomeDeltaTime, 0.1f);
        }

        [TestCase(1.0f, 10.0f, 10.0f, TestName = "Full speed left")]
        [TestCase(0.5f, 10.0f, 5.0f, TestName = "Half speed left")]
        [TestCase(-0.5f, 10.0f, -5.0f, TestName = "Half speed right")]
        [TestCase(-1.0f, 10.0f, -10.0f, TestName = "Full speed right")]
        public void ShouldRespondToHorizontalInput(float input, float maxSpeed, float expectedResult)
        {
            var movement = new PlayerMovement
            {
                HorizontalInput = input,
                MaxHorizontalSpeed = maxSpeed
            };

            movement.UpdateCurrentVelocity(SomeDeltaTime);

            movement.CurrentVelocity.x.Should().BeApproximately(expectedResult, 0.1f);
        }

        [Test]
        [TestCase(0.0f, 1.0f, 1.0f, 10.0f, TestName = "When stopped, after accelerating full left for AccelerationTime, we should be moving full left")]
        [TestCase(0.0f, 0.5f, 1.0f, 5.0f, TestName = "When stopped, after accelerating half left for AccelerationTime, we should be moving half left")]
        [TestCase(0.0f, 1.0f, 0.5f, 5.0f, TestName = "When stopped, after accelerating full left for half AccelerationTime, we should be moving half left")]
        [TestCase(-10.0f, 1.0f, 1.0f, 0.0f, TestName = "When moving full right, after accelerating full left for AccelerationTime, we should be stopped")]
        [TestCase(-10.0f, 1.0f, 2.0f, 10.0f, TestName = "When moving full right, after accelerating full left for double AccelerationTime, we should be moving full left")]
        [TestCase(0.0f, -1.0f, 1.0f, -10.0f, TestName = "When stopped, after accelerating full right for AccelerationTime, we should be moving full right")]
        [TestCase(0.0f, -0.5f, 1.0f, -5.0f, TestName = "When stopped, after accelerating half right for AccelerationTime, we should be moving half right")]
        [TestCase(0.0f, -1.0f, 0.5f, -5.0f, TestName = "When stopped, after accelerating full right for half AccelerationTime, we should be moving half right")]
        [TestCase(10.0f, -1.0f, 1.0f, -0.0f, TestName = "When moving full left, after accelerating full right for AccelerationTime, we should be stopped")]
        [TestCase(10.0f, -1.0f, 2.0f, -10.0f, TestName = "When moving full left, after accelerating full right for double AccelerationTime, we should be moving full right")]
        public void ShouldAccelerateHorizontally(float startXVel, float input, float deltaTime, float expectedXVel)
        {
            var movement = new PlayerMovement
            {
                HorizontalInput = input,
                MaxHorizontalSpeed = 10.0f,
                AccelerationTime = 1.0f, 
                CurrentVelocity = new Vector3(startXVel, 0.0f, 0.0f)
            };

            movement.UpdateCurrentVelocity(deltaTime);

            movement.CurrentVelocity.x.Should().BeApproximately(expectedXVel, 0.1f);
        }

        [Test]
        [TestCase(1.0f, 10.0f, TestName = "Full left acceleration")]
        [TestCase(-1.0f, -10.0f, TestName = "Full right acceleration")]
        public void ShouldNotAccelerateAboveMaxHorizontalVelocity(float input, float expectedXVel)
        {
            var movement = new PlayerMovement
            {
                HorizontalInput = input,
                MaxHorizontalSpeed = 10.0f,
                AccelerationTime = 1.0f
            };

            movement.UpdateCurrentVelocity(10.0f);

            movement.CurrentVelocity.x.Should().BeApproximately(expectedXVel, 0.1f);
        }
    }
}
﻿using UnityEngine;
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

            var result = movement.Update();
            result.y.Should().BeLessThan(0.0f);
        }
    }
}
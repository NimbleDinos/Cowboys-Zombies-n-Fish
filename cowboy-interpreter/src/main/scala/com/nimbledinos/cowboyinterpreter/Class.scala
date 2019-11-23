package com.nimbledinos.cowboyinterpreter

object Class {

  def fromValue(value: String): Either[String, String] =
    value match {
      case "fisher"   ⇒ Right(value)
      case "attacker" ⇒ Right(value)
      case "defender" ⇒ Right(value)
      case "builder"  ⇒ Right(value)
      case _          ⇒ Left(s"${value} is not a valid class")
    }
}

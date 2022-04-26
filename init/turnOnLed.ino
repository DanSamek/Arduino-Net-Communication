String turnOnLed(int pin, int value){
   pinMode(pin, OUTPUT);
   digitalWrite(pin, value);
   return "ok";
}

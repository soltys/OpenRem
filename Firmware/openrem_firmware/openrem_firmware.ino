#include <I2S.h>

#define BUFFER_SIZE 128
bool sending = false;

void setup()
{
    // Baud rate
    Serial.begin(9600);
    while (!Serial)
    {
        ; // wait for serial port to connect. Needed for native USB port only
    }

    // start I2S at 16 kHz with 32-bits per sample
    if (!I2S.begin(I2S_PHILIPS_MODE, 44100, 32))
    {
        Serial.println("Failed to initialize I2S!");
        while (1)
        {
            ; // do nothing
        }
    }
}

byte buffer[BUFFER_SIZE];

void loop()
{
    if (Serial.available() > 0)
    {
        int val = Serial.read();
        if (val == 1)
        {
            sending = true;
        }
        else
        {
            sending = false;
        }
    }

    if (I2S.available())
    {
        I2S.read(buffer, BUFFER_SIZE);
        if (sending)
        {
            Serial.write(buffer, BUFFER_SIZE);
        }
    }
}

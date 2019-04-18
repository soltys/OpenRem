# Knowledge

All information gathered during development of **OpenRem**.

## Wave Format
* [Wave Format](http://soundfile.sapp.org/doc/WaveFormat/)

## Arduino 
* [Wiring for Arduino](https://learn.adafruit.com/adafruit-i2s-mems-microphone-breakout/arduino-wiring-and-test)
* We are sending 44.1khz samples with 32 bit encoding (signed PCM) for each channel, which translates to
    * 4 `bytes` for left 
    * 4 `bytes` for right
    * [most significant bit first](https://en.wikipedia.org/wiki/Bit_numbering#Most_significant_bit)
    * byte order [Little-endian](https://en.wikipedia.org/wiki/Endianness#Little)

## I2S
* [I2S datasheet](https://www.sparkfun.com/datasheets/BreakoutBoards/I2SBUS.pdf)
* [How `I2S.h` library looks like](https://github.com/arduino/ArduinoCore-samd/tree/master/libraries/I2S/src)

## Signal processing
* [C# Data Visualization](https://github.com/swharden/Csharp-Data-Visualization)
    * [FFT](https://github.com/swharden/Csharp-Data-Visualization/blob/master/notes/FFT.md)
    * [Sample using microphone data](https://github.com/swharden/Csharp-Data-Visualization/tree/master/projects/18-09-19_microphone_FFT_revisited)
* [How do I obtain the frequencies of each value in an FFT?](https://stackoverflow.com/questions/4364823/)
* [WPF Sound Visualization Library](https://github.com/jacobjohnston/wpfsvl)

## NAudio
* [NAudio](https://github.com/naudio/NAudio)
* [Understanding Output Devices](https://github.com/naudio/NAudio/blob/master/Docs/OutputDeviceTypes.md)
    * [Getting Audio devices](https://github.com/naudio/NAudio/blob/master/Docs/EnumerateOutputDevices.md)
* [Streaming Audio and Samples](https://github.com/naudio/NAudio/blob/master/Docs/WaveProviders.md)
* [Using RawSourceWaveStream](https://github.com/naudio/NAudio/blob/master/Docs/RawSourceWaveStream.md)

## Service

### WCF

* [Self-Hosting WCF Services with the Autofac WCF Integration](https://alexmg.com/posts/self-hosting-wcf-services-with-the-autofac-wcf-integration)

### gRPC

* [Dotnetifying gRPC: Sane Edition](https://blog.codingmilitia.com/2018/05/19/dotnetifying-grpc-sane-edition)

## MEMS microphone description

Listen to this good news - we now have a breakout board for a super tiny I2S MEMS microphone. Just like 'classic' electret microphones, MEMS mics can detect sound and convert it to voltage, but they're way smaller and thinner. This microphone doesn't even have analog out, its purely digital. The I2S is a small, low cost MEMS mic with a range of about 50Hz - 15KHz, good for just about all general audio recording/detection.

For many microcontrollers, adding audio input is easy with one of our analog microphone breakouts. But as you get to bigger and better microcontrollers and microcomputers, you'll find that you don't always have an analog input, or maybe you want to avoid the noise that can seep in with an analog mic system. Once you get past 8-bit micros, you will often find an I2S peripheral, that can take digital audio data in! That's where this I2S Microphone Breakout comes in.

Instead of an analog output, there are three digital pins: Clock, Data and Left-Right (Word Select) Clock. When connected to your microcontroller/computer, the 'I2S Master' will drive the clock and word-select pins at a high frequency and read out the data from the microphone. No analog conversion required!

The microphone is a single mono element. You can select whether you want it to be on the Left or Right channel by connecting the Select pin to power or ground. If you need stereo, pick up two microphones! You can set them up to be stereo by sharing the Clock, WS and Data lines but having one with Select to ground, and one with Select to high voltage.

This I2S MEMS microphone is bottom ported, so make sure you have the hole in the bottom facing out towards the sounds you want to read. It's a 1.6-3.6V max device only, so not for use with 5V logic (its really unlikely you'd have a 5V-logic device with I2S anyways). Many beginner microcontroller boards don't have I2S, so make sure its a supported interface before you try to wire it up! This microphone is best used with Cortex M-series chips like the Arduino Zero, Feather M0, or single-board computers like the Raspberry Pi.

## How to add Package Download support on DGS Network
"C:\Program Files (x86)\Arduino\java\bin\keytool.exe" -import -alias ciscoumbrella -keystore  "C:\Program Files (x86)\Arduino\java\lib\security\cacerts" -file "C:\tmp\ciscoumbrella.cer"
1. Install Arduino IDE
1. Run mmc
1. File -> Add/Remove Snap in...
1. (on left) Certificates (on center) Add> 
    1. Computer account
    1. Local computer
    1. Ok
1. Go to... Certificates -> Trusted Root Certification Authorities -> Certificates
1. Right click on "Cisco Umbrella Root CA" -> All Tasks... -> Export
1. Next -> **Base-64 encoded X.509**
1. Next -> Save to e.g. c:\tmp\ciscoumbrella.cer
1. Run CMD with Administrative Privilages
1. Run `"C:\Program Files (x86)\Arduino\java\bin\keytool.exe" -import -alias ciscoumbrella -keystore  "C:\Program Files (x86)\Arduino\java\lib\security\cacerts" -file "C:\tmp\ciscoumbrella.cer"`
    1. When ask for password enter `changeit`
    1. confirm with `yes`
1. Run Arduino IDE an download packages

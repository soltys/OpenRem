# Goals
1. Display spectrum graphs on PC
2. Adjust by IMC2 HI's
3. Display spectrum graphs (with correct dB SPLs) on PC

# Hardware
- [ ] Have good connection with microphone
    - [ ] Two microphones should be "moveable"
- [ ] Create port for probe tube
    * Idea use injection needle with cut-off sharp edge should be around 0.5 mm diameter.

# Firmware
- [ ] Find the way, to know exactly which mic, is which.
    * Synchronization packet (?)

# Software
- [ ] Create Arduino - Open Rem - simulator
    - [x] Design common interface for simulated REM and Real one
    - [ ] Integrate solution into OpenRem Platform    
- [ ] Create infrastructure for OpenRem Platform
    - [x] OpenRem.Core (version handling)
    - [x] OpenRem.CommonUi (from Remedy.CommonUI)
    - [ ] OpenRem.UI (AutoFac, ReactiveUI(?))
    - [ ] Import (?) WPF Sound Visualization Library
    - [ ] Import NAudio
    - [ ] Import parts form FakeImc as (back-end)
        * Empty implementation of client
        * Registration
            * NOAH 
            * Standalone
- [ ] Split data from Device into to separate data streams
    * For each 4 bytes
        * two first of them are Right channel
        * two last of them are Left channel
- [ ] USE FFT (Fast Fourier Transform) to obtain proper Spectrum (frequencies/db values)
    * Look at WPF Sound Visualization Library Spectrum analyzer
- [ ] Display curves for both left/right channel
    * Look at WPF Sound Visualization Library Spectrum analyzer    
- [ ] Calibrate microphones to db SPL
    * We could use Verifit to get calibrated signal, then we could map values from microphones to db SPL's
- [ ] Create NOAH module to communicate with FSW by IMC2
    - [ ] Use knowledge from FAKE IMC
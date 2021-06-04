This project converts BBC's iPlayer TTML subtitles into SRT format

There are two alternative solutions in this repo:

* convert.sh

Uses the Saxon XSLT parser and XSLT templates from the SCF (Subtitle conversion framework) to transform the subtitle files.

Usage: Run convert.sh in a directory with ttml files. It will create a new srt file for every ttml file found

* ConvertTTML2SRT

This is a quick .NET Core app I coded which was able to handle the transformation in case SCF failed to do so.
This code needs the TTML file as the first argument and will create a corresponding SRT file as a result

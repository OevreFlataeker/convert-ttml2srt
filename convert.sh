#!/bin/bash

# Based on templates of the SCF (Subtitle conversion framework)
# https://github.com/IRT-Open-Source/scf

# 20210603 Initial version
# Needs libsaxonhe on Ubuntu

JAR=/usr/share/java/Saxon-HE.jar

# Get the dir we're called from
SCRIPT_DIR="$( cd "$( dirname "${BASH_SOURCE[0]}" )" &> /dev/null && pwd )"

for f in *.ttml ;
do
	f2=${f%.ttml}
	echo "Converting file: ${f} from TTML to SRT"
	java -jar ${JAR} -s:"${f}" -xsl:"${SCRIPT_DIR}/TTML2SRTXML.xslt" -o:"${f2}._"
	java -jar ${JAR} -s:"${f2}"._ -xsl:"${SCRIPT_DIR}/SRTXML2SRT.xslt" -o:"${f2}.srt"
	rm "${f2}._"
done


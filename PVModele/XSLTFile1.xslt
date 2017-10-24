<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
  <xsl:output method="xml" indent="yes"/>
  <xsl:template match="/Golfers">
    <Golfers>
      <xsl:apply-templates select="Golfer"/>
    </Golfers>
  </xsl:template>
  <xsl:template match="Golfer">
    <xsl:element name="Golfer">
      <xsl:attribute name="ID" >
        <xsl:value-of select="ID"/>
      </xsl:attribute>
      <xsl:attribute name="Name" >
        <xsl:value-of select="Name"/>
      </xsl:attribute>
      <xsl:attribute name="Birthday" >
        <xsl:value-of select="Birthday"/>
      </xsl:attribute>
    </xsl:element>
  </xsl:template>
</xsl:stylesheet>

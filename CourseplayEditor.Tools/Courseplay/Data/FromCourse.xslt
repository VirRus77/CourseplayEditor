<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:msxsl="urn:schemas-microsoft-com:xslt"
                exclude-result-prefixes="msxsl"
>
  <xsl:output method="xml" encoding="utf-8" indent="yes"/>

  <xsl:template match="@* | node()">
    <xsl:variable name="Name" select="name()" />
    <xsl:choose>
      <xsl:when test="substring-after($Name, 'waypoint')">
        <waypoint>
          <xsl:attribute name="pointX">
            <xsl:value-of select="substring-before(@pos, ' ')"/>
          </xsl:attribute>
          <xsl:attribute name="pointZ">
            <xsl:value-of select="substring-before(substring-after(@pos, ' '), ' ')"/>
          </xsl:attribute>
          <xsl:attribute name="pointY">
            <xsl:value-of select="substring-after(substring-after(@pos, ' '), ' ')"/>
          </xsl:attribute>
          <!--
          <xsl:attribute name="order">
            <xsl:value-of select="substring-after($Name, 'waypoint')"/>
          </xsl:attribute>
          -->
          <!--
          <xsl:apply-templates select="@*[name()!='pos']|node()" />
          -->
          <xsl:apply-templates select="@*|node()" />
        </waypoint>
      </xsl:when>
      <xsl:otherwise>
        <xsl:copy>
          <xsl:apply-templates select="@* | node()"/>
        </xsl:copy>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>
</xsl:stylesheet>
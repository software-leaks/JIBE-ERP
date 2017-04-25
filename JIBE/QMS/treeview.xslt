<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0"  xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:asp="remove">

    <xsl:output method="xml" indent="yes" encoding="utf-8" omit-xml-declaration="yes"/>
    <xsl:variable name="shift-width" select="/treeview/custom-parameters/param[@name='shift-width']/@value"/>
    <xsl:variable name="img-directory" select="/treeview/custom-parameters/param[@name='img-directory']/@value"/>
    <xsl:template match="/treeview">

        <table border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td>
                    <xsl:apply-templates select="folder">
                        <xsl:with-param name="depth" select="1"/>
                    </xsl:apply-templates>
                </td>
            </tr>
        </table>

    </xsl:template>

    <xsl:template match="folder">
        <xsl:param name="depth"/>
        <table border="0" cellspacing="0" cellpadding="0">
            <tr>
                <xsl:if test="$depth>1">
                    <td width="{$shift-width}"></td>
                </xsl:if>
                <td valign="top">
                    <a onclick="toggle(this)" class="folder">
                        <img src="{$img-directory}plus.gif"/>
                        <img src="{$img-directory}{@img}"/>
                        <xsl:value-of select="@title"/>
                    </a>
                    <div>
                        <xsl:attribute name="style">display:none;</xsl:attribute>

                        <xsl:apply-templates select="folder">
                            <xsl:with-param name="depth" select="$depth+1"/>
                        </xsl:apply-templates>
                        <xsl:apply-templates select="leaf"/>

                    </div>
                </td>
            </tr>
        </table>
    </xsl:template>

    <xsl:template match="leaf">
        <table border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td width="{$shift-width}"></td>
                <td valign="top">
                    <a class="leaf">
                        <xsl:attribute name="onclick">
                            selectLeaf('<xsl:value-of select="@title"/>','<xsl:value-of select="@url"/>')
                        </xsl:attribute>
                        <img src="{$img-directory}{@img}"/>
                        <xsl:value-of select="@title"/>
                    </a>
                </td>
            </tr>
        </table>
    </xsl:template>

</xsl:stylesheet>